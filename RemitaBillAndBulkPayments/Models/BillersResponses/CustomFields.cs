using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class CustomFields
    {
        public string id { get; set; }
        public string columnName { get; set; }
        public string columnType { get; set; }
        public List<object> customFieldDropDown { get; set; }
        public string columnLength { get; set; }
        public bool required { get; set; }
        public int dataLoadRuleId { get; set; }
        public string activeStatus { get; set; }
    }

    public class ResponseCustomField<T> where T : class
    {
        public string responseCode { get; set; }
        public List<T> responseData { get; set; }
        public string responseMsg { get; set; }
        public string appVersionCode { get; set; }

        public bool acceptPartPayment { get; set; }
        public bool fixedPrice { get; set; }
        public int fixedAmount { get; set; }
        public string currency { get; set; }
    }
}
