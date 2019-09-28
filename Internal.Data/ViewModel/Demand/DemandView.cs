using Internal.Common.Data;
using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.ViewModel
{
    public class ViewModelDemand:BaseDto
    {
        public string BillCode { get; set; }
        public Guid ID { get; set; }
        public Guid ClientName { get; set; }
        public string Maker { get; set; }

        public string ClientFileName { get; set; }
    }
    public class DemandView : BaseDto
    {
       // public string ID { get; set; }
        public string BillCode { get; set; }
        public string ClientName { get; set; }

        public string ClientNameName { get; set; }
        public string Maker { get; set; }
    }
}
