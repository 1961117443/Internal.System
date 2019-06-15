using Internal.Data.Entity;
using Internal.IRepository;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
