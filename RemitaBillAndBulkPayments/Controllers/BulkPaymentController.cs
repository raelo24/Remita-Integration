using Microsoft.AspNetCore.Mvc;
using RemitaBillAndBulkPayments.Models.BillersResponses;
using RemitaBillAndBulkPayments.Models.BulkRequests;
using RemitaBillAndBulkPayments.Models.BulkResponses;
using RemitaBillAndBulkPayments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RemitaBillAndBulkPayments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkPaymentController : ControllerBase
    {
        private readonly IRemitaService _remitaService;

        public BulkPaymentController(IRemitaService remitaService)
        {
            _remitaService = remitaService;
        }

        [HttpPost("Send")]
        public async Task<ResponseBodyBulk<BulkResponse>> GenerateRRR(BulkRequest details)
        {
            return await _remitaService.SendBulkPayment(details);
        }
    }
}
