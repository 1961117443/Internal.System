using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.IService
{
    public interface ICommentService : IBaseService<Comment>
    {
    }

    //public interface IUserService : IBaseService<UserInfo>
    //{
    //}
    public interface ISysRoleService : IBaseService<SysRole>
    {
    }
    //public interface ISysPermissionService : IBaseService<SysPermission>
    //{
    //}
    public interface ISysUserRoleService : IBaseService<SysUserRole>
    {
    }
    public interface ISysRolePermissionService : IBaseService<SysRolePermission>
    {
    }
}
