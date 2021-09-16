using RemitaBillAndBulkPayments.Models.BillerCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class ValidateResponse : Validate
    {
        public string status { get; set; }
        public double amountDue { get; set; }
    }
}
