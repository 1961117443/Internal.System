using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Entity
{
    /// <summary>
    /// 接口权限表
    /// </summary>
    public class SysPermission
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string LinkUrl { get; set; }
        public string HttpMethod { get; set; }
    }
}
