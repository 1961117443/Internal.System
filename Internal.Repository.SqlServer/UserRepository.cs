using Internal.Data.Entity;
using Internal.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Repository.SqlServer
{
    public class UserRepository : BaseRepository<UserInfo>, IUserRepository
    {
        public UserRepository()
        { 
        }

        public Task<List<SysPermission>> GetUserPermission(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SysRole>> GetUserRole(Guid Id)
        {
            // sysRoleRepository.GetSelect().Where(w=> SqlFunc.Subqueryable<>)
            var q= Db.Queryable<SysRole>()
                 .Where(r => SqlFunc.Subqueryable<SysUserRole>()
                                  .Where(ur => SqlFunc.Subqueryable<UserInfo>()
                                  .Where(u => u.ID.Equals(Id) && u.ID.Equals(ur.UserID)).Any()).Any());
            return await q.ToListAsync();
        }
    }
}
