using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.BillersResponses
{
    public class Response<T> where T : class
    {
        public string responseCode { get; set; }
        public List<T> responseData { get; set; }
        public string responseMsg { get; set; }
        public string appVersionCode { get; set; }
    }

    public class ResponseBodyBulk<T> where T : class
    {
        public string responseCode { get; set; }
        public T responseData { get; set; }
        public string responseMsg { get; set; }
    }
}
