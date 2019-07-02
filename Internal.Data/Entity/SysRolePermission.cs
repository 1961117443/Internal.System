using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Entity
{
    public class SysRolePermission
    {
        public int ID { get; set; }
        public Guid RoleID { get; set; }
        public Guid PermissionID { get; set; }
    }
}
