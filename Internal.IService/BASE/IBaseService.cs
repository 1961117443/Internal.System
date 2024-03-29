﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IService
{ 
    public interface IBaseService<TEntity> where TEntity : class
    {
        /// <summary>
        /// 查询单个实体对象
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        Task<TEntity> QueryByIDAsync(object objId);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddAsync(TEntity model);
        /// <summary>
        /// 根据主键删除对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(object id);
        /// <summary>
        /// 根据对象进行删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TEntity model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(object[] ids);
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(TEntity model); 
        /// <summary>
        /// 更新实体对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns> 
        Task<bool> UpdateAsync(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="whereExpression">过滤表达式</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression);
        /// <summary>
        /// 查询实体
        /// </summary>
        /// <param name="whereExpression">过滤表达式</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);

        /// <summary>
        /// 获取分页数据，不会返回页数
        /// </summary>
        /// <param name="whereExpression">过滤表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryPageAsync(int intPageIndex, int intPageSize, string strOrderByFileds = null);

        /// <summary>
        /// 获取分页数据，不会返回页数
        /// </summary> 
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryPageAsync(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null);

        /// <summary>
        /// 获取分页数据，不会返回页数
        /// </summary>
        /// <param name="whereExpression">过滤表达式</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="orderByFiledExpression">排序字段</param>
        /// <param name="asc">顺序</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryPageExAsync(Expression<Func<TEntity, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, Expression<Func<TEntity, object>> orderByFiledExpression = null, bool asc = true);
    }
}
