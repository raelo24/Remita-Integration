using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace RemitaBillAndBulkPayments.Models.BulkResponses
{
    public class BulkData
    {
        [Key]
        [JsonIgnore]
        public int id { get; set; }
        public string batchRef { get; set; }
        public double totalAmount { get; set; }
        public string authorizationId { get; set; }
        public string transactionDate { get; set; }

        [JsonIgnore]
        public int BulkId { get; set; }

        [ForeignKey("BulkId")]
        [JsonIgnore]
        public BulkResponse BulkResponse { get; set; }

    }

    public class BulkResponse
    {
        [Key]
        [JsonIgnore]
        public int BulkId { get; set; }

        public string status { get; set; }
        public string message { get; set; }

        [JsonIgnore]
        public BulkData? data { get; set; }
       
    }
}
