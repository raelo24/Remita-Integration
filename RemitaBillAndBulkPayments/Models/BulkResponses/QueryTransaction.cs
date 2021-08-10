using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BulkResponses
{
    public class Transaction
    {
        public int amount { get; set; }
        public string transactionRef { get; set; }
        public string paymentDate { get; set; }
        public string paymentStatus { get; set; }
        public string statusMessage { get; set; }
    }

    public class QueryTransaction
    {
        public string batchRef { get; set; }
        public int totalAmount { get; set; }
        public double feeAmount { get; set; }
        public string authorizationId { get; set; }
        public string transactionDate { get; set; }
        public string transactionDescription { get; set; }
        public string currency { get; set; }
        public string paymentStatus { get; set; }
        public List<Transaction> transactions { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }


}
