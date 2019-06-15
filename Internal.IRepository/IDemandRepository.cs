using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.IRepository
{
    public interface IDemandRepository : IBaseRepository<Demand>
    {
    }
    public interface IUserRepository : IBaseRepository<UserInfo>
    {
    }
}
