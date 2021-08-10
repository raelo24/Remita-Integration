using Microsoft.Extensions.Configuration;
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
    }
    public class RemitaProxy : IRemitaProxy
    {
        private IHttpClientHelper _httpClient;
        private IConfiguration _configuration;
        public RemitaProxy(IHttpClientHelper httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> Get(string url)
        {
            return await _httpClient.GetWithBody(url, GetPublicKey());
        }

        public async Task<string> Post(string url, string requestBody)
        {
            return await _httpClient.PostwithBody(url, GetPublicKey(), requestBody);
        }

        private Dictionary<string, string> GetPublicKey()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            var key = _configuration.GetValue<string>("RemitaCredentials:publicKey");
            if (key == null) throw new ArgumentNullException("public key is not set in the config file");
            header.Add("publicKey", key);
            return header;
        }
    }
}
