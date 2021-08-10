using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RemitaBillAndBulkPayments.Models.BillersResponses;
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
        private readonly ILogger<BillPaymentController> _logger;

        public BillPaymentController(IRemitaService remitaService, ILogger<BillPaymentController> logger)
        {
            _remitaService = remitaService;
            _logger = logger;
        }

        // GET: api/<BillPaymentController>
        [HttpGet]
        public async Task<Response<Billers>> GetBillers()
        {
            return await _remitaService.GetBiller();
        }

        // GET api/<BillPaymentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BillPaymentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<BillPaymentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BillPaymentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
