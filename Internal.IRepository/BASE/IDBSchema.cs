using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IRepository
{
    /// <summary>
    /// 实体类架构相关
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public partial interface IDBSchema
    {
        Task<DataSet> GetSqlSchema(string tableName);
    }
}
