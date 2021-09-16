using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RemitaBillAndBulkPayments.Models.BillerCommon;
using RemitaBillAndBulkPayments.Models.BillersRequests;
using RemitaBillAndBulkPayments.Models.BillersResponses;
using RemitaBillAndBulkPayments.Models.EFContext;
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
    public class BillPaymentController : ControllerBase
    {
        private readonly IRemitaService _remitaService;

        public BillPaymentController(IRemitaService remitaService)
        {
            _remitaService = remitaService;
        }

        // GET: api/<BillPaymentController>
        [HttpGet("GetBillers")]
        public async Task<Response<Billers>> GetBillers()
        {
            return await _remitaService.GetBiller();
        }

        [HttpGet("GetService")]
        public async Task<Response<List<Service>>> GetService(string biller)
        {
            return await _remitaService.GetService(biller);
        }

        [HttpGet("GetCustomFields")]
        public async Task<ResponseCustomField> GetCustomFields(string billId)
        {
            return await _remitaService.GetCustomFields(billId);
        }       

        [HttpPost("ValidateRequest")] 
        public async Task<Response<ValidateResponse>> ValidateRequest(Validate details)
        {
            return await _remitaService.Validate(details);
        }

        [HttpPost("GenerateRRR")]
        public async Task<Response<GenerateRRR>> GenerateRRR(Validate details)
        {
            return await _remitaService.GenerateRRR(details);
        }

        [HttpGet("GetRRRDetails")]
        public async Task<Response<RRRDetails>> GetRRRDetails(string rrr)
        {
            return await _remitaService.GetRRRDetails(rrr);
        }


        [HttpGet("PaymentStatus")]
        public async Task<Response<PaymentStatus>> PaymentStatus(string transactionId)
        {
            return await _remitaService.PaymentStatus(transactionId);
        }

        [HttpPost("BillPaymentNotificatiion")]
        public async Task<Response<BillPaymentResponse>> BillPaymentNotificatiion(BillPaymentRequest details)
        {
            if (ModelState.IsValid)
            {
                return await _remitaService.BillPaymentNotification(details);
            }

            return new Response<BillPaymentResponse> { responseCode = "1001", responseMsg = "Request is invalid" };
        }

    }
}
