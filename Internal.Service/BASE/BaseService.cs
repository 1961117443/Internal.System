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

        public virtual async Task<int> AddAsync(TEntity model)
        {
            return await Repository.AddAsync(model);
        }

        public virtual async Task<bool> DeleteAsync(TEntity model)
        {
            return await Repository.DeleteAsync(model);
        }

        public virtual async Task<bool> DeleteByIdAsync(object id)
        {
            return await Repository.DeleteByIdAsync(id);
        }

        public async virtual Task<bool> DeleteByIdsAsync(object[] ids)
        {
            return await Repository.DeleteByIdsAsync(ids);
        }

        public async virtual Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Repository.QueryAsync(whereExpression);
        }

        public async virtual Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Repository.QueryAsync(whereExpression, strOrderByFileds);
        }

        public async virtual Task<TEntity> QueryByIDAsync(object objId)
        {
            return await Repository.QueryByIDAsync(objId);
        }

        public async virtual Task<List<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            return await Repository.QueryPageAsync(whereExpression,intPageIndex, intPageSize, strOrderByFileds);
        }

        public async Task<List<TEntity>> QueryPageAsync(int intPageIndex, int intPageSize, string strOrderByFileds = null)
        {
            return await Repository.QueryPageAsync(null, intPageIndex, intPageSize, strOrderByFileds);
        }

        public async virtual Task<List<TEntity>> QueryPageExAsync(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, Expression<Func<TEntity, object>> orderByFiledExpression = null, bool asc = true)
        {
            return await Repository.QueryPageExAsync(whereExpression, intPageIndex, intPageSize,orderByFiledExpression,asc);
        }

        public async virtual Task<bool> UpdateAsync(TEntity model)
        {
            return await Repository.UpdateAsync(model);
        }

        public async virtual Task<bool> UpdateAsync(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            return await Repository.UpdateAsync(entity, lstColumns, lstIgnoreColumns, strWhere);
        }
    }
}
