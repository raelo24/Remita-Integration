using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RemitaBillAndBulkPayments.Constants;
using RemitaBillAndBulkPayments.Models.Auth;
using RemitaBillAndBulkPayments.Models.BillersRequests;
using RemitaBillAndBulkPayments.Models.EFContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Services
{
    public interface IUtil
    {
        string PaymentHash(BillPaymentRequest request);
        string GenerateTransactionId();
        string GeneratePaymentAuthCode();
        string GenerateBatchRef();
        Task<TokenData> GetTokenFromDb(); //useful if the tokens have long lifespan of many days
        Task<(bool,string, string, TokenData)> GetTokenRealTime(); //useful for only short live tokens
    }

    public class Util : IUtil
    {
        private readonly DatabaseContext _dbContext;
        private readonly ILogger<Util> _logger;
        private readonly IHttpClientHelper _httpClient;
        private readonly RemitaConstants _appsettings;

        public Util(DatabaseContext context, ILogger<Util> logger, IHttpClientHelper httpClient, IOptions<RemitaConstants> appsettings )
        {
            _dbContext = context;
            _logger = logger;
            _httpClient = httpClient;
            _appsettings = appsettings.Value;
        }

        public string PaymentHash(BillPaymentRequest request)
        {
            var secretKey = _appsettings.BillSecretKey;
            string values = request.rrr + request.amountDebitted + request.fundingSource + request.debittedAccount + request.paymentAuthCode + secretKey;
            byte[] inBytes = Encoding.UTF8.GetBytes(values);

            using (SHA512 crypt = new SHA512Managed())
            {
                var encypted = crypt.ComputeHash(inBytes);

                //convert the string to byte array
                var builder = new StringBuilder(128);

                foreach (var item in encypted)
                {
                    builder.Append(item.ToString("X2"));
                }

                return builder.ToString();
            };           
        }

        public string GeneratePaymentAuthCode()
        {
            string authCode = string.Empty;

            try
            {
                StringBuilder sb = new StringBuilder();
                Random ran = new Random();

                for (int i = 0; i < 19; i++)
                {
                    sb.Append(ran.Next(0, 9));
                }

                authCode = sb.ToString();
                //repeat if necessary
                if (_dbContext.PaymentRequests.Any(x=>x.paymentAuthCode==authCode))
                {
                    return GeneratePaymentAuthCode();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Excepton in GeneratePaymentAuthCode", ex);
            }

            return authCode;
        }

        public string GenerateTransactionId()
        {
            string tranId = string.Empty;

            try
            {
                StringBuilder sb = new StringBuilder();
                Random ran = new Random();

                for (int i = 0; i < 15; i++)
                {
                    sb.Append(ran.Next(0, 9));
                }

                tranId = sb.ToString();

                //repeat if necessary
                var intTranId = int.Parse(tranId);
                if (_dbContext.PaymentRequests.Any(x => x.transactionId == intTranId))
                {
                    return GenerateTransactionId();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Excepton in GenerateTransactionId", ex);
            }

            return tranId;
        }
        public string GenerateBatchRef()
        {
            string batchRef = string.Empty;

            try
            {
                StringBuilder sb = new StringBuilder();
                Random ran = new Random();

                for (int i = 0; i < 19; i++)
                {
                    sb.Append(ran.Next(0, 9));
                }

                batchRef = sb.ToString();

                //repeat if necessary
                if (_dbContext.BulkRequests.Any(x => x.batchRef == batchRef))
                {
                    return GenerateBatchRef();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Excepton in GenerateBatchRef", ex);
            }

            return batchRef;
        }

        public async Task<TokenData> GetTokenFromDb()
        {
            try
            {
                var token = _dbContext.TokenData.SingleOrDefault();

                if (token != null)
                {
                    //check if the token has expired
                    if (DateTime.Compare(token.ExpireDateTime, DateTime.Now) > 0) //it as expired cos the expiry is earlier than now
                    {
                        return token; //still valid
                    }
                }

                //request and save another token
                var URL = _appsettings.BulkPaymentBase + _appsettings.GetToken;
                var response = await _httpClient.PostwithBody(URL, null, JsonConvert.SerializeObject(new TokenRequest { username = _appsettings.BulkUsername, password = _appsettings.BulkPassword }));
                var desResponse = JsonConvert.DeserializeObject<TokenResponse>(response);

                //check that response is not null
                if (desResponse != null)
                {
                    //get the needed token data
                    var tokenData = desResponse.data.FirstOrDefault();
                    if (tokenData == null) return token;

                    tokenData.ExpireDateTime = DateTime.Now.AddMilliseconds(tokenData.expiresIn);

                    if (token != null)
                    {
                        token.accessToken = tokenData?.accessToken;
                        token.expiresIn = tokenData.expiresIn;
                        token.ExpireDateTime = tokenData.ExpireDateTime;
                    }
                    else
                    {
                        token = new TokenData { accessToken = tokenData.accessToken, expiresIn = tokenData.expiresIn, ExpireDateTime = tokenData.ExpireDateTime };
                        await _dbContext.TokenData.AddAsync(token);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError("GetToken", ex);
            }

            return null;
        }

        public async Task<(bool, string, string, TokenData)> GetTokenRealTime()
        {
            try
            {
                //request and save another token
                var URL = _appsettings.BulkPaymentBase + _appsettings.GetToken;
                var response = await _httpClient.PostwithBody(URL, null, JsonConvert.SerializeObject(new TokenRequest { username = _appsettings.BulkUsername, password = _appsettings.BulkPassword }));
                var desResponse = JsonConvert.DeserializeObject<TokenResponse>(response);

                //check that response is not null
                if (desResponse?.data != null)
                {
                    //get the needed token data
                    return (true, "success", desResponse.status,  desResponse?.data?.FirstOrDefault());
                }

                return (false, $"Acquiring token error. {desResponse?.message}", desResponse?.status, null);
            }
            catch (Exception ex)
            {
                _logger.LogError("GetTokenRealTime", ex);
                return  (false, ex.Message, "1001", null);
            }
        }
    }
}
