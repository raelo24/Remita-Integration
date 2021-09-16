using Microsoft.Extensions.Logging;
using RemitaBillAndBulkPayments.Models.BillersResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using RemitaBillAndBulkPayments.Models.BillerCommon;
using RemitaBillAndBulkPayments.Models.BillersRequests;
using System.Web.Http.ModelBinding;
using System.Net.Http;
using System.Net;
using RemitaBillAndBulkPayments.Models.EFContext;
using RemitaBillAndBulkPayments.Models.BulkRequests;
using RemitaBillAndBulkPayments.Models.BulkResponses;
using Microsoft.Extensions.Options;
using RemitaBillAndBulkPayments.Constants;
using Microsoft.AspNetCore.Mvc;

namespace RemitaBillAndBulkPayments.Services
{
    public interface IRemitaService
    {
        Task<Response<Billers>> GetBiller();
        Task<Response<List<Service>>> GetService(string biller);
        Task<ResponseCustomField> GetCustomFields(string billId);
        Task<Response<ValidateResponse>> Validate(Validate details);
        Task<Response<GenerateRRR>> GenerateRRR(Validate details);
        Task<Response<RRRDetails>> GetRRRDetails(string rrr);

        Task<Response<PaymentStatus>> PaymentStatus(string rrr);
        Task<Response<BillPaymentResponse>> BillPaymentNotification(BillPaymentRequest request);
        Task<ResponseBodyBulk<BulkResponse>> SendBulkPayment(BulkRequest request);


    }
    public class RemitaService : IRemitaService
    {
        private ILogger<RemitaService> _logger;
        private IRemitaProxy _remitaProxy;
        private readonly IOptions<RemitaConstants> _remitaConstants;
        IOptions<QuestConstants> _questConstants;
        private readonly IUtil _util;
        private readonly DatabaseContext _context;

        public RemitaService(ILogger<RemitaService> logger, IRemitaProxy remitaProxy,
            IOptions<RemitaConstants> appsettings,
             IOptions<QuestConstants> questConstants,
            IUtil util, DatabaseContext context)
        {
            _logger = logger;
            _remitaProxy = remitaProxy;
            _remitaConstants = appsettings;
            _util = util;
            _context = context;
            _questConstants = questConstants;
        }

        public async Task<Response<Billers>> GetBiller()
        {
            try
            {
                var response = await _remitaProxy.Get(_remitaConstants.Value.BaseUrl + _remitaConstants.Value.Billers);
                var desResponse = JsonConvert.DeserializeObject<Response<Billers>>(response);
                return desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<Billers>() { responseMsg = ex.Message, responseCode = "1001" };
            }           
        }

        public async Task<Response<List<Service>>> GetService(string biller)
        {
            try
            {
                if (string.IsNullOrEmpty(biller))
                {
                    return new Response<List<Service>>() { responseMsg = "Biller is not indicated", responseCode = "999" };
                }

                var url = $"{_remitaConstants.Value.BaseUrl}{biller}/{_remitaConstants.Value.Services})";
                var response = await _remitaProxy.Get(url);
                var desResponse = JsonConvert.DeserializeObject<Response<List<Service>>>(response);
                return response == null ? new Response<List<Service>>() : desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<List<Service>>() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }

        public async Task<ResponseCustomField> GetCustomFields(string billId)
        {
            try
            {
                if (string.IsNullOrEmpty(billId))
                {
                    return new ResponseCustomField() { responseMsg = "Biller ID  is not indicated", responseCode = "999" };
                }

                var url = _remitaConstants.Value.BaseUrl + _remitaConstants.Value.Services + $"/{billId}";
                var response = await _remitaProxy.Get(url);
                var desResponse = JsonConvert.DeserializeObject<ResponseCustomField>(response);
                return response == null ? new ResponseCustomField() : desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ResponseCustomField() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }

        public async Task<Response<ValidateResponse>> Validate(Validate details)
        {
            try
            {
                var url = _remitaConstants.Value.BaseUrl + _remitaConstants.Value.Validate;
                string requestBody = JsonConvert.SerializeObject(details);
                var response = await _remitaProxy.Post(url, requestBody);
                var desResponse = JsonConvert.DeserializeObject<Response<ValidateResponse>>(response);
                return response == null ? new Response<ValidateResponse>() : desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<ValidateResponse>() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }

        public async Task<Response<GenerateRRR>> GenerateRRR(Validate details)
        {
            try
            {
                var url = _remitaConstants.Value.BaseUrl + _remitaConstants.Value.GenerateRRR;
                string requestBody = JsonConvert.SerializeObject(details);
                var response = await _remitaProxy.Post(url, requestBody);
                var desResponse = JsonConvert.DeserializeObject<Response<GenerateRRR>>(response);
                return response == null ? new Response<GenerateRRR>() : desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<GenerateRRR>() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }

        public async Task<Response<RRRDetails>> GetRRRDetails(string rrr)
        {
            try
            {
                var url = _remitaConstants.Value.BaseUrl + _remitaConstants.Value.RRDetails + $"/{rrr}";
                var response = await _remitaProxy.Get(url);
                var desResponse = JsonConvert.DeserializeObject<Response<RRRDetails>>(response);
                return response == null ? new Response<RRRDetails>() : desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<RRRDetails>() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }
              

       
        public async Task<Response<BillPaymentResponse>> BillPaymentNotification(BillPaymentRequest request)
        {
            try
            {
                var url = _remitaConstants.Value.BaseUrl + _remitaConstants.Value.billPayment;

                request.transactionId = long.Parse(_util.GenerateTransactionId());
                request.paymentAuthCode = _util.GeneratePaymentAuthCode();                
                request.fundingSource = _remitaConstants.Value.fundingSource;
                request.hash = _util.PaymentHash(request); 

                string requestBody = JsonConvert.SerializeObject(request);

                //prepare the extra headers
                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("transactionId", request.transactionId.ToString());
                headers.Add("TXN_HASH", request.hash);

                //save this request
                await _context.PaymentRequest.AddAsync(request);

                var response = await _remitaProxy.PostWithExtraHeaders(url, requestBody, headers); 
                var desResponse = JsonConvert.DeserializeObject<Response<BillPaymentResponse>>(response);

                var responseToSave = desResponse.responseData?.FirstOrDefault();
                if(desResponse.responseData != null)
                {
                    responseToSave.responseCode = desResponse?.responseCode;
                    responseToSave.responseMsg = desResponse?.responseMsg;
                    responseToSave.transactionId = request.transactionId;

                    await _context.PaymentResponse.AddAsync(responseToSave);
                }               

                await _context.SaveChangesAsync();

                //save the response
                return response == null ? new Response<BillPaymentResponse>() : desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<BillPaymentResponse>() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }      

        public async Task<Response<PaymentStatus>> PaymentStatus(string transactionId)
        {
            try
            {
                var url = _remitaConstants.Value.BaseUrl + _remitaConstants.Value.paymentStatus + $"/{transactionId}";
                var response = await _remitaProxy.Get(url);
                var desResponse = JsonConvert.DeserializeObject<Response<PaymentStatus>>(response);
                return response == null ? new Response<PaymentStatus>() : desResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Response<PaymentStatus>() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }

        public async Task<ResponseBodyBulk<BulkResponse>> SendBulkPayment(BulkRequest request)
        {
            try
            {
                //add bearer token              
                var url = _remitaConstants.Value.BulkPaymentBase + _remitaConstants.Value.SendBulk;
                request.originalBankCode = _questConstants.Value.BankCode;
                request.sourceBankCode = request.originalBankCode;
                request.batchRef = _util.GenerateBatchRef();

               

                //serialize the request
                string requestBody = JsonConvert.SerializeObject(request);

                //save request to database
                // await _context.BulkRequest.AddAsync(request);

                //get the token
                var (isSuccessful, message, status, token) = await _util.GetTokenRealTime();

                if (!isSuccessful)
                    return new ResponseBodyBulk<BulkResponse>() { responseCode = status, responseMsg = message };

                Dictionary<string, string> headers = new Dictionary<string, string>();
                headers.Add("Authorization", $"Bearer {token?.accessToken}");
                var response = await _remitaProxy.PostWithOwnHeader(url, requestBody, headers);

                var desResponse = JsonConvert.DeserializeObject<ResponseBodyBulk<dynamic>>(response);

                //save the response to db
                if (desResponse?.responseData != null)
                {
                    var data = JsonConvert.DeserializeObject<BulkResponse>(desResponse.responseData);

                    return new ResponseBodyBulk<BulkResponse>() 
                    {
                        responseMsg = desResponse.responseMsg, 
                        responseCode = desResponse.responseCode,
                        responseData = data
                    };

                }

                //  await _context.SaveChangesAsync();
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new ResponseBodyBulk<BulkResponse>() { responseMsg = ex.Message, responseCode = "1001" };
            }
        }

        //public async Task<HttpResult>> GetReceipt(string rrr)
        //{
        //    try
        //    {
        //        string uniqueId = "";
        //        var url = _appsettings.Value.BaseUrl + _configuration.GetValue<string>("RemitaCredentials:publicKey") + $"/{rrr}/{uniqueId}/rest.reg";
        //        var response = await _remitaProxy.Get(url);
        //        var desResponse = JsonConvert.DeserializeObject<Response<PaymentStatus>>(response);
        //        return response == null ? new Response<PaymentStatus>() : desResponse;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex.ToString());
        //        return new Response<PaymentStatus>() { responseMsg = "Some errors occured", responseCode = "1001" };
        //    }
        //}
    }
}
