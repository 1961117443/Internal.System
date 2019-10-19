using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data
{
    public class ProductDiagram:BaseModel<Guid>
    {
        public string IconName { get; set; }
        public byte[] Image { get; set; }
    }
}
