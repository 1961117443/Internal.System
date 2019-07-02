using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internal.App.Options
{
    public class TokenOptions : IOptions<TokenOptions>
    {
        public TokenOptions Value => this;

        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
    }
     
}
