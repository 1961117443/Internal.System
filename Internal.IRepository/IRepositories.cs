using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IRepository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
    }

    public interface ICommentRepository : IBaseRepository<Comment>
    {
    }

    public interface IUserRepository : IBaseRepository<UserInfo>
    {
        Task<List<SysRole>> GetUserRole(Guid Id);
        Task<List<SysPermission>> GetUserPermission(Guid Id);
    }
    public interface ISysRoleRepository : IBaseRepository<SysRole>
    {
    }
    public interface ISysPermissionRepository : IBaseRepository<SysPermission>
    {
    }
    public interface ISysUserRoleRepository : IBaseRepository<SysUserRole>
    {
    }
    public interface ISysRolePermissionRepository : IBaseRepository<SysRolePermission>
    {
    }

}
