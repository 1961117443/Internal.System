using Autofac.Extras.DynamicProxy; 
using Internal.Common.Attributes;
using Internal.Common.Data;
using Internal.Data;
using Internal.Data.Entity;
using Internal.IService.AOP;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks; 
namespace Internal.IService
{
    /// <summary>
    /// 单据操作接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Intercept(typeof(ServiceInterceptorAOP))]
    public interface IBillService<T>
    {
        //object PrimaryKeyValue { get; set; }

        //T Current { get; }
        /// <summary>
        /// 保存单据
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>  
        Task<ApiResult> PostAsync(BaseDto dto);
        /// <summary>
        /// 获取单据列表
        /// </summary>
        /// <param name="where">过滤条件</param>
        /// <returns></returns>
        Task<ApiResult> GetListAsync(Expression<Func<T, bool>> where);
        /// <summary>
        /// 根据id删除数据
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task<ApiResult> DeleteAsync(object id);
        /// <summary>
        /// 根据id获取一条记录
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        Task<ApiResult> GetAsync(object id);
    } 
}
