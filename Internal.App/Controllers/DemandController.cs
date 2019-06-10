using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internal.Common.Core;
using Internal.Data.Entity;
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
    public class DemandController : Controller
    {
        private readonly IDemandService demandService;

        public DemandController(IDemandService demandService)
        {
            this.demandService = demandService;
        }

        /// <summary>
        /// 获取 需求或者bug
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var list = await demandService.QueryPage(null);
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
        [HttpPost("api/post")]
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