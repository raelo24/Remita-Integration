using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class BillPaymentNotification
    {
        public string rrr { get; set; }
        public double totalAmount { get; set; }
        public int balanceDue { get; set; }
        public string paymentRef { get; set; }
        public string paymentDate { get; set; }
        public string debittedAccount { get; set; }
        public double amountDebitted { get; set; }
    }
}
