using Internal.Common.Helpers;
using Internal.Data.Entity;
using Internal.IRepository;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Service
{
    public class SysPermissionService : BaseService<SysPermission>, ISysPermissionService
    {
        private readonly ISysPermissionRepository sysPermissionRepository;
        private readonly ISysUserRoleRepository sysUserRoleRepository;
        private readonly ISysRolePermissionRepository sysRolePermissionRepository;

        protected override IBaseRepository<SysPermission> Repository => this.sysPermissionRepository;

        public SysPermissionService(ISysPermissionRepository sysPermissionRepository,ISysUserRoleRepository sysUserRoleRepository,ISysRolePermissionRepository sysRolePermissionRepository)
        {
            this.sysPermissionRepository = sysPermissionRepository;
            this.sysUserRoleRepository = sysUserRoleRepository;
            this.sysRolePermissionRepository = sysRolePermissionRepository;
        }

        public async Task<bool> HasAuth(string linkUrl, string httpMethod, string userId)
        {
            var permission =await sysPermissionRepository.Single(w => w.LinkUrl.Equals(linkUrl) && w.HttpMethod.Equals(httpMethod));
            //没有做权限控制
            if (permission==null)
            {
                return true;
            } 
            List<Guid> userRoles = new List<Guid>();
            foreach (var item in await sysUserRoleRepository.Query(w => w.UserID.Equals(userId.toGuid())))
            {
                userRoles.Add(item.RoleID);
            }
            var rolePermission = await sysRolePermissionRepository.Single(w => w.PermissionID.Equals(permission.ID) && userRoles.Contains(w.RoleID));
            return rolePermission!=null;
        }
    }
}
