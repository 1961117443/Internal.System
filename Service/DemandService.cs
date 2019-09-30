using FreeSql;
using Internal.Data;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Internal.Data.Uility;
using AutoMapper;
using Admin.Dto.Demand;
using FreeSql.Internal.Model;
using System.Reflection;
using System.Linq;

namespace Admin.Service
{
    public class DemandService : IDemandService
    {
        #region 属性 字段
        private readonly IFreeSql freeSql;
        private readonly IMapper mapper;

        protected ISelect<Demand> Queryable
        {
            get
            {
                return this.freeSql.Select<Demand>();
            }
        }
        #endregion

     
        #region 构造函数
        public DemandService(IFreeSql freeSql,IMapper mapper)
        {
            this.freeSql = freeSql;
            this.mapper = mapper;
        }

        public DemandService()
        {
        }
        #endregion

        public async Task<int> AddAsync(Demand model)
        {
            Console.WriteLine(model);

            return 0;
        }

        public Task<bool> DeleteAsync(Internal.Data.Entity.Demand model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdsAsync(object[] ids)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<List<DemandView>>> GetListAsync(PageParam param)
        {
            ApiResult<List<DemandView>> apiResult = new ApiResult<List<DemandView>>(); 
            var query = this.Queryable.LeftJoin(a => a.ClientFile.ID == a.ClientName).Page(param.PageIndex, param.PageSize);
            apiResult.Data =await query.ToListAsync<DemandView>();
            return apiResult;
        }

        public Task<List<Internal.Data.Entity.Demand>> QueryAsync(System.Linq.Expressions.Expression<Func<Internal.Data.Entity.Demand, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public Task<List<Internal.Data.Entity.Demand>> QueryAsync(System.Linq.Expressions.Expression<Func<Internal.Data.Entity.Demand, bool>> whereExpression, string strOrderByFileds)
        {
            throw new NotImplementedException();
        }

        public async Task<Internal.Data.Entity.Demand> QueryByIDAsync(object objId)
        {
            return await freeSql.Select<Demand>(objId).ToOneAsync();
        }

        public Task<List<Demand>> QueryPageAsync(int intPageIndex, int intPageSize, string strOrderByFileds = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Internal.Data.Entity.Demand>> QueryPageAsync(System.Linq.Expressions.Expression<Func<Internal.Data.Entity.Demand, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<Internal.Data.Entity.Demand>> QueryPageExAsync(System.Linq.Expressions.Expression<Func<Internal.Data.Entity.Demand, bool>> whereExpression, int intPageIndex = 0, int intPageSize = 20, System.Linq.Expressions.Expression<Func<Internal.Data.Entity.Demand, object>> orderByFiledExpression = null, bool asc = true)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Demand model)
        {
            var updater = this.freeSql.Update<Demand>().SetSource(model);
            int res = await updater.ExecuteAffrowsAsync();
            return res > 0;
        }

        public Task<bool> UpdateAsync(Internal.Data.Entity.Demand entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            throw new NotImplementedException();
        }

        public async Task<List<Demand>> GetPageAsync(PageParam param)
        {
            var query = this.Queryable
                .LeftJoin(a => a.ClientFile.ID == a.ClientName) 
                .Page(param.PageIndex, param.PageSize);

            var where = param.PageParamToExpression<Demand>();
            query = query.WhereIf(where != null, where);

            var selectExpression = EntityHelper<Demand>.GetQueryMember(); 

            return selectExpression != null ? await query.ToListAsync(selectExpression) : await query.ToListAsync();
        }

        [Obsolete]
        public async Task<int> UpdateAsync(Demand model,  string[] columns)
        {
            var updater = this.freeSql.Update<Demand>(model);
            int res = await updater.ExecuteAffrowsAsync();
            return res  ;
        }

        public async Task<bool> UpdateAsync(Demand model, Expression<Func<Demand, Demand>> func)
        {
            var updater = this.freeSql.Update<Demand>(model.ID);
            if (func!=null)
            {
                updater = updater.Set(func);
            }
            updater.Set(a => a.ModifyDate , DateTime.Now);
            int res = await updater.ExecuteAffrowsAsync();
            return res > 0; 
        }

        public async Task<ApiResult> PostAffrowsAsync(DemandView dto)
        {
            ApiResult res = new ApiResult(); 
            var source = await this.QueryByIDAsync(dto.Id);
          //  this.freeSql.Update()
            
            //  dto.AssignValuesToEntity(source);
            var exp = source.UpdateExpression(dto);

            var updater= this.freeSql.Update<Demand>(source.ID).Set(exp).Set(a => a.ModifyDate, DateTime.Now);

            int i = await updater.ExecuteAffrowsAsync();
            return res;
        }
    }
}
