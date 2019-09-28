using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internal.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Internal.App.Controllers.v2
{
    using AutoMapper;
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
    using Internal.App.Filters;
    using Internal.Common.Core;
    using Internal.Data;
    using Internal.Data.Entity;
    using Internal.Data.Uility;
    using Internal.Data.ViewModel;
    using System.Linq.Expressions;

    [Route("demands")]
    [ApiController]
    public class DemandsController : ControllerBase
    {
        private readonly IDemandService demandService;
        private readonly IMapper mapper;
        private readonly IBillService<Demand> billOperation;

        public DemandsController(IDemandService demandService,IMapper mapper, IBillService<Demand> billOperation)
        {
            this.demandService = demandService;
            this.mapper = mapper;
            this.billOperation = billOperation;
        }
        // GET: api/Demands
        [HttpGet]
        public async Task<IActionResult> Get(int index, int size)
        { 
            List<Demand> demands = await demandService.QueryPageAsync(index,size);
            var data = mapper.Map<List<DemandView>>(demands);

            var res = new ApiResult<List<DemandView>>(data); 
            return Ok(res);
        }
        // GET: api/Demands/list
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            Demand entity = new Demand(); 
            QueryField queryField = new QueryField();
            queryField.FieldName = "ClientFile.ClientName";

            PageParam pageParam = new PageParam();

            QueryParam param = new QueryParam();
            param.Field = "ClientNameName";
            param.Value = "托田";
            param.Logic = LogicEnum.Like; 
            pageParam.Params.Add(param);
            IEnumerable<Demand> data = await demandService.GetPageAsync(pageParam);
            ApiTableResult<DemandView> res = new ApiTableResult<DemandView>();

            data.ToDtoList<Demand,DemandView>(); 
            res.Data = this.mapper.Map<List<DemandView>>(data);

          
            return Ok(res);
        }

        // GET: api/Demands/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        { 
            return "value";
        }

        // POST: api/Demands
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DemandView value)
        {
            string id = "";
            var demand = this.mapper.Map<Demand>(value);
            var tm = this.mapper.ConfigurationProvider.FindTypeMapFor<DemandView, Demand>();
            
            int r=  await demandService.AddAsync(demand);
            ApiResult apiResult = new ApiResult();
            if (r>0)
            {
                apiResult.msg = "添加成功！";
            }
            else
            {
                apiResult.msg = "添加失败！";
            }

            var res = await billOperation.PostAsync(value);
            return Ok(apiResult);
        }

        // PUT: api/Demands/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody]DemandView dto)
        {
            var affrowRes = await this.demandService.PostAffrowsAsync(dto);

            ApiResult apiResult = new ApiResult();
            var source = await this.demandService.QueryByIDAsync(dto.Id);
            // var exp1 = demand.UpdateExpression(dto);
            ////dto创新一个新的entity
            //var data2 = this.mapper.Map<Demand>(dto);
            ////原来的entity复制到新的entity
            //var newdata = this.mapper.Map(source, data2);
            Demand copy = new Demand();
            copy = this.mapper.Map(source, copy);
            //var copy = source.Clone();
            dto.AssignValuesToEntity(copy);

            var exp1 = source.UpdateExpression(copy);

            var b = await demandService.UpdateAsync(source);


            var data = this.mapper.Map(dto, source);
            var exp= source.UpdateExpression(dto);
            var res = await billOperation.PostAsync(dto);
            return Ok(apiResult);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("confirm")]
        [RecordLockAttribute(TableName = "Demand", Type = typeof(Demand), Action = RecordLockType.Lock)]
        [RecordLockAttribute(TableName = "Demand", Type = typeof(Demand), Action = RecordLockType.UnLock)]
        public async Task<IActionResult> Confirm(/*[FromBody] DemandView value*/)
        {
            ApiResult apiResult = new ApiResult();
            object id = "";
            var demand= await this.demandService.QueryByIDAsync(id);
            if (demand == null)
            {
                apiResult.msg = "资源不存在！"; 
            }
            else
            {
                var demandold = await this.demandService.QueryByIDAsync(id);
                demand.Audit = "admin";
                demand.AuditDate = DateTime.Now;
                Expression<Func<Demand, Demand>> exp = a=> new Demand();// demandold.UpdateExpression(demand);
                bool b = await demandService.UpdateAsync(demand, exp);
            }
            return Ok(apiResult);

        }

        [HttpGet("edit")]
        [RecordLockAttribute(Order =1,TableName = "Demand",Type = typeof(Demand), Action = RecordLockType.Lock)]
        public async Task<IActionResult> Edit()
        {
            ApiResult apiResult = new ApiResult();
            return Ok(apiResult);
        }
    }
}
