using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BulkResponses
{
    public class Data
    {
        public string batchRef { get; set; }
        public int totalAmount { get; set; }
        public string authorizationId { get; set; }
        public string transactionDate { get; set; }
    }

    public class BulkResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
    }
}
