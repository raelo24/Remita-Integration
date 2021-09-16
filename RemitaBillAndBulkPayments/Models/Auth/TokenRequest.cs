using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemitaBillAndBulkPayments.Models.Auth
{
    public class TokenRequest
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class TokenResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<TokenData> data { get; set; }
    }
    public class TokenData
    {
        public int id { get; set; }
        public string accessToken { get; set; }
        public int expiresIn { get; set; }
        public DateTime ExpireDateTime { get; set; }

    }
}
