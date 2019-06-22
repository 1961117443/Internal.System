using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Internal.App.Authority;
using Internal.Common.Core;
using Internal.Common.Helpers;
using Internal.Data.Entity;
using Internal.Data.ViewModel;
using Internal.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Internal.App.Controllers
{
    /// <summary>
    /// 需求管理
    /// </summary>
    [Route("api/demand")]
    [ApiController]
    public class DemandController : BaseController
    {
        private readonly IDemandService _demandService;
        private readonly IMapper _mapper;
        private readonly IAspNetUser aspNetUser;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="demandService"></param>
        /// <param name="mapper"></param>
        /// <param name="aspNetUser">当前登陆用户</param>
        public DemandController(IDemandService demandService,IMapper mapper,IAspNetUser aspNetUser)
        {
            this._demandService = demandService;
            this._mapper = mapper;
            this.aspNetUser = aspNetUser;
        }

        /// <summary>
        /// 获取 需求或者bug
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        [HttpGet("{id}")]
      //  [Authorize]
        public async Task<IActionResult> Get(string id)
        {
            var vd = await _demandService.QueryByID(id); 
            return ApiResult(vd);
        }
        /// <summary>
        /// 添加需求
        /// </summary>
        /// <param name="editModel"></param>
        /// <returns></returns>
        [HttpPost("post")]
        public async Task<IActionResult> Post([FromBody]DemandEditModel editModel)
        { 

            var demand = _mapper.Map<Demand>(editModel);
            var dt = DateTime.Now; 
            demand.BillCode = dt.ToString("yyMMddHHmmss");
            demand.MakeDate = dt;
            demand.Maker = this.aspNetUser.Name;

            int r = await _demandService.Add(demand);
            return ApiResult(r>0? "提交成功！" : "提交失败！");
        }

        /// <summary>
        /// 获取需求列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetPageList(int pageIndex,int pageSize)
        {
            var list = await _demandService.QueryPage(null,pageIndex,pageSize);
            var data = _mapper.Map<List<DemandCardModel>>(list);
            return ApiResult(data);
        }

        /// <summary>
        /// 修改需求
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DemandEditModel editModel)
        {
            var res = new ResultModel<string>();

            if (editModel.ID.Equals(Guid.Empty))
            {
                res.Data = "需求不存在！";
                return Ok(res);
            }
            var demand = await _demandService.QueryByID(editModel.ID);
            demand.Describe = editModel.Describe;
            demand.Presenter = editModel.Presenter;
            demand.RecordDate = editModel.RecordDate;

            bool r = await _demandService.Update(demand, new List<string>() { "Describe", "Presenter", "RecordDate" });
            if (r)
            {
                res.Data = r ? "保存成功！" : "失败成功！";
            }
            return Ok(res);
        }

        /// <summary>
        /// 需求审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("audit")]
        public async Task<IActionResult> Audit(string id)
        {
            var res = new ResultModel<DemandCardModel>();
            var demand = await _demandService.QueryByID(id); 
            if (demand.Audit.IsEmpty())
            {
                res.Message = "需求已审核！";
                res.Data = this._mapper.Map<DemandCardModel>(demand);  
                return ApiResult(res);
            }
            if (!demand.Rejector.IsEmpty())
            {
                res.Message = "需求已拒批！";
                res.Data = this._mapper.Map<DemandCardModel>(demand); 
                return ApiResult(res);
            }
            demand.Audit = this.aspNetUser.Name;
            demand.AuditDate = DateTime.Now;
            var r =await this._demandService.Update(demand);
            if (await this._demandService.Update(demand))
            {
                res.Message = "审核成功！";
                res.Data = this._mapper.Map<DemandCardModel>(demand); ;
            }
            else
            {
                res.Message = "审核失败！";
            }

            return ApiResult(res);
        }

        /// <summary>
        /// 需求反审
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("unaudit")]
        public async Task<IActionResult> UnAudit(string id)
        {
            return ApiResult("反审成功");
        }

        /// <summary>
        /// 删除需求
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/demand/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var res = new ResultModel<string>();
            res.Data = await _demandService.DeleteById(id) ? "删除成功！":"";
            return Ok(res);
        }
    }
}