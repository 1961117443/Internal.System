using AutoMapper;
using Internal.Common.Core;
using Internal.Data;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IRepository;
using Internal.IService;
using Internal.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Service
{
    public  class DemandService: BaseService<Demand>, IDemandService
    {
        #region 数据访问层
        private readonly IDemandRepository _demandRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper mapper;
        private readonly IEnumerable<IDemandRepository> demandRepositories;
        #endregion

        public DemandService(IDemandRepository demandRepository, ICustomerRepository customerRepository,IMapper mapper,IEnumerable<IDemandRepository> demandRepositories)
        {
            this._demandRepository = demandRepository;
            this._customerRepository = customerRepository;
            this.mapper = mapper;
            this.demandRepositories = demandRepositories;
        }

        protected override IBaseRepository<Demand> Repository => _demandRepository;

        public async Task<ApiResult<List<DemandView>>> GetListAsync(PageParam param)
        {
            

            ApiResult<List<DemandView>> result = new ApiResult<List<DemandView>>();
            //List<Demand> list = null;
            //foreach (var repository in this.demandRepositories)
            //{
            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();
            //    list = await repository.QueryPageAsync(param);
            //    stopwatch.Stop();
            //    Console.WriteLine($"仓储层：{repository.GetType()}，耗时:{stopwatch.ElapsedMilliseconds}毫秒。");

            //}
            var list = await this._demandRepository.QueryPageAsync(param);
            result.Data = this.mapper.Map<List<DemandView>>(list);
            var model = this.mapper.Map<List<ViewModelDemand>>(list);
            return result;  
        }

        public Task<List<Demand>> GetPageAsync(PageParam param)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult> PostAffrowsAsync(DemandView model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Demand model,  string[] columns)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Demand model, Expression<Func<Demand, Demand>> func)
        {
            throw new NotImplementedException();
        }
    }
}
