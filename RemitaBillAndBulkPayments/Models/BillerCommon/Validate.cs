using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillerCommon
{
    public class Value
    {
        [Required]
        public string value { get; set; }
        public double quantity { get; set; }
        public double amount { get; set; }
    }

    public class CustomField
    {
        [Required]
        public string id { get; set; }
        public List<Value> values { get; set; }
    }

    public class Validate
    {
        public List<CustomField> customFields { get; set; }
        [Required]
        public string billId { get; set; }
        [Required]
        public double amount { get; set; }
        [Required]
        public string payerPhone { get; set; }
        public string currency { get; set; }
        [Required]
        public string payerName { get; set; }
        [Required]
        public string payerEmail { get; set; }
    }
}
