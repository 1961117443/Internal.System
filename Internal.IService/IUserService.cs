using Internal.Common.Core;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IService
{
    public interface IUserService : IBaseService<UserInfo>
    {
        /// <summary>
        /// 根据用户id获取用户的角色
        /// </summary>
        /// <param name="userId"></param>
        Task GetUserRole(string userId);
        /// <summary>
        /// 根据用户id获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        Task GetUserPermission(string userId);

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        Task<CommunicationModel<UserInfo>> UserLogin(string code, string pwd);
    }
}
