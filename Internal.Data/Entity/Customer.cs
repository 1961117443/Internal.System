using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Entity
{
   public partial  class Customer
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 客户编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Name { get; set; }
         
    }
    public partial class Customer
    {
        public Customer Parent { get; set; }
    }

}
