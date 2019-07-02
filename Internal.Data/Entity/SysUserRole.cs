using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Entity
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class SysUserRole
    {
        public int ID { get; set; }
        public Guid RoleID { get; set; }
        public Guid UserID { get; set; }
    }
}
