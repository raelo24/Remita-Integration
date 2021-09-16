using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersRequests
{
    public class BillPaymentRequest
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string rrr { get; set; }
        [Required]
        public string incomeAccount { get; set; }
        [Required]
        public string debittedAccount { get; set; }
        public string paymentAuthCode { get; set; }
        public string paymentChannel { get; set; } = "Internet Banking";
        public string tellerName { get; set; }
        public string branchCode { get; set; }
        public string amountDebitted { get; set; }
        public string fundingSource { get; set; }
        public string hash { get; set; }

        //extra fiels for the request
        [JsonIgnore]
        public long transactionId { get; set; }
        [JsonIgnore]
        public DateTime RequestDateTime { get; set; } = DateTime.Now;
    }
}
