using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Constants
{
    public class RemitaConstants
    {
        public string BillPublicKey { get; set; }
        public string BillSecretKey { get; set; }
        public string BaseUrl { get; set; }
        public string BulkPaymentBase { get; set; }
        public string SendBulk { get; set; }
        public string GetToken { get; set; }
        public string fundingSource { get; set; }
        public string Billers { get; set; }
        public string Services { get; set; }
        public string Validate { get; set; }
        public string GenerateRRR { get; set; }
        public string RRDetails { get; set; }
        public string billPayment { get; set; }
        public string paymentStatus { get; set; }
        public string getReceipt { get; set; }
        public string BulkUsername { get; set; }
        public string BulkPassword { get; set; }
    }
}
