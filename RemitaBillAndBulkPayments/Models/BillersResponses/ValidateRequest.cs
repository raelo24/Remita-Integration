using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class Value
    {
        public string value { get; set; }
        public int amount { get; set; }
        public int quantity { get; set; }
    }

    public class CustomField
    {
        public string id { get; set; }
        public List<Value> values { get; set; }
    }

    public class ValidateRequest
    {
        public List<CustomField> customFields { get; set; }
        public string billId { get; set; }
        public int amount { get; set; }
        public string payerPhone { get; set; }
        public string currency { get; set; }
        public string payerName { get; set; }
        public string payerEmail { get; set; }
        public double amountDue { get; set; }
        public string status { get; set; }
    }
}
