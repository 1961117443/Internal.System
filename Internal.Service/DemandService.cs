using Internal.Data.Entity;
using Internal.IRepository; 
using Internal.IService;
using Internal.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Service
{
    public  class DemandService: BaseService<Demand>, IDemandService
    {
        #region 数据访问层
        private readonly IDemandRepository _demandRepository;
        private readonly ICustomerRepository _customerRepository; 
        #endregion

        public DemandService(IDemandRepository demandRepository, ICustomerRepository customerRepository)
        {
            this._demandRepository = demandRepository;
            this._customerRepository = customerRepository;
        }

        protected override IBaseRepository<Demand> Repository => _demandRepository;
    }
}
