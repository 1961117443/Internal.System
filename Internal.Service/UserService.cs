using Internal.Common.Core;
using Internal.Common.Helpers;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IRepository;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Service
{
    public class UserService : BaseService<UserInfo>, IUserService
    {
        private readonly IUserRepository _userRepository;

        protected override IBaseRepository<UserInfo> Repository => _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task GetUserRole(string userId)
        {
            List<SysRole> sysRoles = await _userRepository.GetUserRoleAsync(userId.toGuid()); 
        }

        public Task GetUserPermission(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CommunicationModel<UserInfo>> UserLogin(string code, string pwd)
        {
            CommunicationModel<UserInfo> communicationModel = new CommunicationModel<UserInfo>();
            var users = await this._userRepository.QueryAsync(w => w.UserCode == code);
            if (!users.Any())
            {
                communicationModel.Success = false;
                communicationModel.Message = "账号不存在";
            }
            else
            {
                var user = users.FirstOrDefault(w => w.UserCode == code && w.PassWord == MD5($"{pwd}{w.ID}"));
                if (user == null)
                {
                    communicationModel.Message = "密码错误"; ;
                    communicationModel.Success = false;
                }
                else
                {
                    communicationModel.Success = true;
                    communicationModel.Data = user;
                }
            }

            return communicationModel;
        }

        public string MD5(string str)
        {
            byte[] bytes = MD5(System.Text.Encoding.ASCII.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            foreach (byte num in bytes)
            {
                sb.AppendFormat("{0:x2}", num);
            }
            return sb.ToString();
        } 
        public byte[] MD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }
    }
}
