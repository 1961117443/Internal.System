using Internal.Data.Entity;
using Internal.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Repository.SqlServer
{
    public class UserRepository : BaseRepository<UserInfo>, IUserRepository
    {
    }
}
