using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RemitaBillAndBulkPayments.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Services
{
    public interface IRemitaProxy
    {
        Task<string> Get(string url);
        Task<string> Post(string url, string requestBody);
        Task<string> PostWithExtraHeaders(string url, string requestBody, Dictionary<string, string> extraHeaders);
        Task<string> PostWithOwnHeader(string url, string requestBody, Dictionary<string, string> headers);
    }
    public class RemitaProxy : IRemitaProxy
    {
        private IHttpClientHelper _httpClient;
        private IConfiguration _configuration;
        private readonly IOptions<RemitaConstants> _remitaConstants;

        public RemitaProxy(IHttpClientHelper httpClient, IConfiguration configuration, IOptions<RemitaConstants> options)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _remitaConstants = options;
        }

        public async Task<string> Get(string url)
        {
            return await _httpClient.GetWithBody(url, GetPublicKey());
        }

        public async Task<string> Post(string url, string requestBody)
        {
            return await _httpClient.PostwithBody(url, GetPublicKey(), requestBody);
        }

        public async Task<string> PostWithExtraHeaders(string url, string requestBody, Dictionary<string, string> extraHeaders)
        {
            var mainHeaders = GetPublicKey();

            //add more headers to the existing headers
            foreach (var item in extraHeaders)
            {
                mainHeaders.Add(item.Key, item.Value);
            }

            return await _httpClient.PostwithBody(url, mainHeaders, requestBody);
        }

        public async Task<string> PostWithOwnHeader(string url, string requestBody, Dictionary<string, string> headers)
        {

            return await _httpClient.PostwithBody(url, headers, requestBody);
        }


        private Dictionary<string, string> GetPublicKey()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            var key = _remitaConstants.Value.BillPublicKey ?? throw new ArgumentNullException("public key is not set in the config file");
            header.Add("publicKey", key);
            return header;
        }
    }
}
