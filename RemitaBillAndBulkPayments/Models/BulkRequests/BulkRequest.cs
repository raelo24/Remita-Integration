using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BulkRequests
{
    public class Transaction
    {
        public int amount { get; set; }
        public string transactionRef { get; set; }
        public string destinationAccount { get; set; }
        public string destinationAccountName { get; set; }
        public string destinationBankCode { get; set; }
        public string destinationNarration { get; set; }
    }

    public class BulkRequest
    {
        public string batchRef { get; set; }
        public int totalAmount { get; set; }
        public string sourceAccount { get; set; }
        public string sourceAccountName { get; set; }
        public string sourceBankCode { get; set; }
        public string currency { get; set; }
        public string sourceNarration { get; set; }
        public string originalAccountNumber { get; set; }
        public string originalBankCode { get; set; }
        public string customReference { get; set; }
        public List<Transaction> transactions { get; set; }
    }


}
