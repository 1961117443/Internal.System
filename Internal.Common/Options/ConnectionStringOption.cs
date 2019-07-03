using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Options
{
    public class ConnectionStringOption : IOptions<ConnectionStringOption>
    {
        public ConnectionStringOption Value => this;
        public string ConnectionString { get; set; }
        public string ConnectionName { get; set; }
    }
}
