using Internal.Data;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Internal.IService
{
    public interface IDemandService: IBaseService<Demand>
    {
        Task<ApiResult<List<DemandView>>> GetListAsync(PageParam param);

        Task<List<Demand>> GetPageAsync(PageParam param);

        Task<int> UpdateAsync(Demand model, string[] columns);
        Task<bool> UpdateAsync(Demand model, Expression<Func<Demand, Demand>> func);

        /// <summary>
        /// 保存操作
        /// 只更新有更改的列
        /// </summary>
        /// <param name="model">dto对象</param>
        /// <returns></returns>
        Task<ApiResult> PostAffrowsAsync(DemandView model);
    }
}
