using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IService
{

    public interface IDBSchemaService
    {
        //Dictionary<string, object> GetSqlSchema(string tableName);
        /// <summary>
        /// 数据库实体生成C#类
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Task<string> DbToCSharp(string tableName);
    }
    public interface ICommentService : IBaseService<Comment>
    {
    }

    //public interface IUserService : IBaseService<UserInfo>
    //{
    //}
    public interface ISysRoleService : IBaseService<SysRole>
    {
    }
    //public interface ISysPermissionService : IBaseService<SysPermission>
    //{
    //}
    public interface ISysUserRoleService : IBaseService<SysUserRole>
    {
    }
    public interface ISysRolePermissionService : IBaseService<SysRolePermission>
    {
    }
}
