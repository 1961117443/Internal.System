using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IService
{
    public interface ISysPermissionService : IBaseService<SysPermission>
    {
        /// <summary>
        /// 判断用户是否拥有权限
        /// </summary>
        /// <param name="linkUrl">接口地址</param>
        /// <param name="httpMethod">请求方式</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        Task<bool> HasAuth(string linkUrl,string httpMethod, string userId);

        /// <summary>
        /// 获取所有的接口权限
        /// </summary>
        /// <returns></returns>
        Task<List<SysPermission>> GetAllPermission();
    }
}
