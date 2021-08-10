using Microsoft.Extensions.Logging;
using RemitaBillAndBulkPayments.Models.BillersResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace RemitaBillAndBulkPayments.Services
{
    public interface IRemitaService
    {
        Task<Response<Billers>> GetBiller();
    }

    public class RemitaService : IRemitaService
    {
        private ILogger<RemitaService> _logger;
        private IRemitaProxy _remitaProxy;
        private readonly IConfiguration _configuration;

        public RemitaService(ILogger<RemitaService> logger, IRemitaProxy remitaProxy, IConfiguration configuration)
        {
            _logger = logger;
            _remitaProxy = remitaProxy;
            _configuration = configuration;
        }

        public async Task<Response<Billers>> GetBiller()
        {
            var url = _configuration.GetValue<string>("RemitaCredentials:BaseUrl") + _configuration.GetValue<string>("RemitaCredentials:Billers");
            var response = await _remitaProxy.Get(url);
            var desResponse = JsonConvert.DeserializeObject<Response<Billers>>(response);
            return response == null ?  new Response<Billers>() : desResponse;
        }
    }
}
