using Internal.Common.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Admin.Dto.Demand
{

    public class DemandDto:BaseDto
    {
        public string BillCode { get; set; }
        public string ClientName { get; set; }

        public string ClientNameName { get; set; }
        public string Maker { get; set; }
    }
}
