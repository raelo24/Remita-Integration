using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class RRRDetails
    {
        public string rrr { get; set; }
        public double amountDue { get; set; }
        public string payerEmail { get; set; }
        public string payerName { get; set; }
        public string payerPhone { get; set; }
        public string description { get; set; }
        public string currency { get; set; }
        public string type { get; set; }
        public bool acceptPartPayment { get; set; }
        public string frequency { get; set; }
        public string payerAccountNumber { get; set; }
        public object maxNoOfDebits { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
    }
}
