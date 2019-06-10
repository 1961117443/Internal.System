using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Internal.Common.Core;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Internal.App.Controllers
{
    /// <summary>
    /// 需求&BUG控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DemandController : BaseController
    {
        private readonly IDemandService _demandService;
        private readonly IMapper _mapper;

        public DemandController(IDemandService demandService,IMapper mapper)
        {
            this._demandService = demandService;
            this._mapper = mapper;
        }

        /// <summary>
        /// 获取 需求或者bug
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var list = await _demandService.QueryPage(null);
            var data = _mapper.Map<List<DemandViewModel>>(list);
            Demand d = new Demand()
            {
                BillCode = "123",
                CustomerID = Guid.NewGuid(),
                MakeDate = DateTime.Now,
                Maker = "admin",
                Presenter = "admin",
                RecordDate = DateTime.Now
            }; 
            return Json(new ResultData<Demand>() { Data = d });
        }
        /// <summary>
        /// 提交需求
        /// </summary>
        /// <param name="demand"></param>
        /// <returns></returns>
        [HttpPost("post")]
        public IActionResult Post(Demand demand)
        {
            ResultData result = new ResultData()
            {
                Status = 0,
                Message = "成功"
            };
            return   Json(result);
        }
    }
}