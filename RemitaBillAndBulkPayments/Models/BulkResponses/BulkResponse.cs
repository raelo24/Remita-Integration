using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RemitaBillAndBulkPayments.Models.BulkResponses
{
    public class BulkData
    {
        public string batchRef { get; set; }
        public double totalAmount { get; set; }
        public string authorizationId { get; set; }
        public string transactionDate { get; set; }
    }

    public class BulkResponse
    {
        [JsonIgnore]
        public int id { get; set; }

        public string status { get; set; }
        public string message { get; set; }
        [NotMapped]
        public BulkData? data { get; set; }
        [JsonIgnore]
        public string datastr { get; set; }
    }
}
