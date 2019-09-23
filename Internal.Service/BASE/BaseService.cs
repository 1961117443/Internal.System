using Internal.IRepository;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Service
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    { 

        protected abstract IBaseRepository<TEntity> Repository { get;  }

        public virtual async Task<int> Add(TEntity model)
        {
            return await Repository.Add(model);
        }

        public virtual async Task<bool> Delete(TEntity model)
        {
            return await Repository.Delete(model);
        }

        public virtual async Task<bool> DeleteById(object id)
        {
            return await Repository.DeleteById(id);
        }

        public async virtual Task<bool> DeleteByIds(object[] ids)
        {
            return await Repository.DeleteByIds(ids);
        }

        public async virtual Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Repository.Query(whereExpression);
        }

        public async virtual Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Repository.Query(whereExpression, strOrderByFileds);
        }

        public async virtual Task<TEntity> QueryByID(object objId)
        {
            return await Repository.QueryByID(objId);
        }

        public async virtual Task<List<TEntity>> QueryPage(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await Repository.QueryPage(whereExpression,intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<List<TEntity>> QueryPage(int intPageIndex, int intPageSize, string strOrderByFileds = null)
        {
            return await Repository.QueryPage(null, intPageIndex, intPageSize, strOrderByFileds);
        }

        public async virtual Task<List<TEntity>> QueryPageEx(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, Expression<Func<TEntity, object>> orderByFiledExpression = null, bool asc = true)
        {
            return await Repository.QueryPageEx(whereExpression, intPageIndex, intPageSize,orderByFiledExpression,asc);
        }

        public async virtual Task<bool> Update(TEntity model)
        {
            return await Repository.Update(model);
        }

        public async virtual Task<bool> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            return await Repository.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }
    }
}
