using Internal.Common.Cache;
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
        #region 属性字段
        private static readonly object lock_service = new object();
        private readonly ISysPermissionRepository sysPermissionRepository;
        private readonly ISysUserRoleRepository sysUserRoleRepository;
        private readonly ISysRolePermissionRepository sysRolePermissionRepository;
        private readonly IRedisCacheManager redisCacheManager; 
        #endregion

        protected override IBaseRepository<SysPermission> Repository => this.sysPermissionRepository;

        public SysPermissionService(ISysPermissionRepository sysPermissionRepository, ISysUserRoleRepository sysUserRoleRepository, ISysRolePermissionRepository sysRolePermissionRepository, IRedisCacheManager redisCacheManager)
        {
            this.sysPermissionRepository = sysPermissionRepository;
            this.sysUserRoleRepository = sysUserRoleRepository;
            this.sysRolePermissionRepository = sysRolePermissionRepository;
            this.redisCacheManager = redisCacheManager;
        }


        /// <summary>
        /// 判断用户是否拥有权限
        /// </summary>
        /// <param name="linkUrl">api接口地址</param>
        /// <param name="httpMethod">请求方式</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public async Task<bool> HasAuth(string linkUrl, string httpMethod, string userId)
        {
            //获取所有权限
            List<SysPermission> list = await GetAllPermission();
            //当前的接口权限
            var permission = list.FirstOrDefault(w => w.LinkUrl.Equals(linkUrl) && w.HttpMethod.Equals(httpMethod));
            //没有做权限控制
            if (permission == null)
            {
                return true;
            }
            //获取角色权限 
            List<string> rolePermissions = await GetRolePermission(userId);
            return rolePermissions.Contains(permission.ID.ToString());
        }

        /// <summary>
        /// 获取用户的角色权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<List<string>> GetRolePermission(string userId)
        {
            var key = CacheConstantKey.Create(CacheConstantKey.PERMISSION_ROLE_PERMISSION, userId);
            if (redisCacheManager.Exists(key))
            {
                return redisCacheManager.Get(key).Split(",").ToList();
            }
            List<string> userRoles = await GetUserRole(userId);
            List<string> rolePermissions = new List<string>();
            foreach (var item in await sysRolePermissionRepository.QueryAsync(w => userRoles.Contains(w.RoleID.ToString())))
            {
                rolePermissions.Add(item.PermissionID.ToString());
            }
            redisCacheManager.Set(key, string.Join(",", rolePermissions));
            return rolePermissions;
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<List<string>> GetUserRole(string userId)
        {
            var key = CacheConstantKey.Create(CacheConstantKey.PERMISSION_USER_ROLE, userId);
            if (redisCacheManager.Exists(key))
            {
                return redisCacheManager.Get(key).Split(",").ToList();
            }
            List<string> userRoles = new List<string>();
            foreach (var item in await sysUserRoleRepository.QueryAsync(w => w.UserID.Equals(userId.toGuid())))
            {
                userRoles.Add(item.RoleID.ToString());
            }
            redisCacheManager.Set(key, string.Join(",", userRoles));
            return userRoles;
        }

        /// <summary>
        /// 获取所有的用户权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<SysPermission>> GetAllPermission()
        {
            List<SysPermission> permissions = null;
            if (redisCacheManager.Exists(CacheConstantKey.PERMISSION))
            {
                permissions = redisCacheManager.Get<List<SysPermission>>(CacheConstantKey.PERMISSION);

            }
            else
            {
                permissions = await sysPermissionRepository.QueryAsync(w => !w.ID.Equals(Guid.Empty)) ?? new List<SysPermission>();
                redisCacheManager.Set(CacheConstantKey.PERMISSION, permissions);
            }
            return permissions;
        }
    }
}
