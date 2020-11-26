using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Request
{
    public class RefreshAccessTokenRequest
    {
        public string RefreshToken { get; set; }
    }
}
