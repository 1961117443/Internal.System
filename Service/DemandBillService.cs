using AutoMapper;
using Internal.Common.Data;
using Internal.Data;
using Internal.Data.Entity;
using Internal.Data.Uility;
using Internal.Data.ViewModel;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Service
{
    public class DemandBillService : IBillService<Demand>
    {
        private readonly IFreeSql fsql;
        private readonly IMapper mapper;

        public DemandBillService(IFreeSql Fsql,IMapper mapper)
        {
            fsql = Fsql;
            this.mapper = mapper;
        }

        //public object PrimaryKeyValue { get; set; }

        //private Demand _current;
        //public Demand Current
        //{
        //    get
        //    {
        //        if (_current==null)
        //        {
        //            if (PrimaryKeyValue!=null)
        //            {
        //                _current = this.fsql.Select<Demand>().Where(a => a.ID.Equals(PrimaryKeyValue)).First();
        //            }
        //        }
        //        return _current;
        //    }
        //}

        public Task<ApiResult> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult> GetAsync(object id)
        {
            //PrimaryKeyValue = id;
            var _current = await this.fsql.Select<Demand>().Where(a => a.ID.Equals(id)).FirstAsync();
             var dto = mapper.Map<DemandView>(_current);
            ApiResult res = new ApiResult<DemandView>(dto);
            return res; 
        }

        public async Task<ApiResult> GetListAsync(Expression<Func<Demand, bool>> where)
        { 
            var query= fsql.Select<Demand>().LeftJoin(a => a.ClientFile.ID == a.ClientName).Page(1, 20);
            var list =await query.ToListAsync();

            ApiResult res = new ApiTableResult<Demand>(list);

            return res;
        }

        public async Task<ApiResult> PostAsync(BaseDto dto)
        {  
            ApiResult res = new ApiResult();
            var source = await fsql.Select<Demand>().Where(a => a.ID.Equals(dto.Id)).FirstAsync(); 

            var exp = source.UpdateExpression(dto);

            var updater = this.fsql.Update<Demand>(source.ID).Set(exp).Set(a => a.ModifyDate, DateTime.Now);

            int i = await updater.ExecuteAffrowsAsync();
            return res;
        }
    }
}
