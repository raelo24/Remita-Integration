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
        public List<customFieldDropDown> customFieldDropDown { get; set; }
        public string columnLength { get; set; }
        public bool required { get; set; }
        public int dataLoadRuleId { get; set; }
        public string activeStatus { get; set; }
    }

    public class ResponseCustomField : Response<CustomFields>
    {
        public bool acceptPartPayment { get; set; }
        public bool fixedPrice { get; set; }
        public double fixedAmount { get; set; }
        public string currency { get; set; }
    }

    public class customFieldDropDown{
        public string description { get; set; }
        public string fixedprice { get; set; }
        public string unitprice { get; set; }
        public object itemphoto { get; set; }
        public object itemname { get; set; }
        public string code { get; set; }
        public string accountid { get; set; }
        public string id { get; set; }
    }
}
