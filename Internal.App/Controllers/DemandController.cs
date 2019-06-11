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
            var vd = await _demandService.QueryPage(w=>w.ID.Equals(id)); 
            return SuccessResult(vd);
        }
        /// <summary>
        /// 提交需求
        /// </summary>
        /// <param name="demand"></param>
        /// <returns></returns>
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody]DemandViewModel demand)
        { 

            return SuccessResult("保存成功");
        }

        /// <summary>
        /// 获取需求列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet(Name ="list")]
        public async Task<IActionResult> GetPageList(int pageIndex,int pageSize)
        {
            var list = await _demandService.QueryPage(null,pageIndex,pageSize);
            var data = _mapper.Map<List<DemandViewModel>>(list);
            return SuccessResult(data);
        } 

        /// <summary>
        /// 录入需求
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns> 
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DemandViewModel demand)
        {
            return SuccessResult("保存成功");
        }

        /// <summary>
        /// 需求审核
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns> 
        [HttpPut(Name ="audit")]
        public async Task<IActionResult> Audit(string id)
        {
            return SuccessResult("审核成功");
        }

        /// <summary>
        /// 需求反审
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpPut(Name = "unaudit")]
        public async Task<IActionResult> UnAudit(string id)
        {
            return SuccessResult("反审成功");
        }
    }
}