using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class GenerateRRR
    {
        public string amountDue { get; set; }
        public string rrr { get; set; }
        public string type { get; set; }
    }
}
