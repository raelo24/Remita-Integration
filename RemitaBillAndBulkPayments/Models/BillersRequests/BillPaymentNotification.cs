using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersRequests
{
    public class BillPaymentNotification
    {
        public string rrr { get; set; }
        public string incomeAccount { get; set; }
        public string debittedAccount { get; set; }
        public string paymentAuthCode { get; set; }
        public string paymentChannel { get; set; }
        public string tellerName { get; set; }
        public string branchCode { get; set; }
        public string amountDebitted { get; set; }
        public string fundingSource { get; set; }
        public string hash { get; set; }
    }
}
