using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class BillPaymentResponse
    {
        [Key]
        [JsonIgnore]
        public int id { get; set; }
        public string rrr { get; set; }
        public double totalAmount { get; set; }
        public double balanceDue { get; set; }
        public string paymentRef { get; set; }
        public string paymentDate { get; set; }
        public string debittedAccount { get; set; }
        public double amountDebitted { get; set; }
        public long transactionId { get; set; }


        //extra field
        [JsonIgnore]
        public string responseMsg  { get; set; }
        [JsonIgnore]
        public string responseCode { get; set; }

    }
}
