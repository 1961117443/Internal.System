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
    using Internal.Common.Core;
    using Internal.Data.Entity;
    using Internal.Data.ViewModel;

    [Route("demands")]
    [ApiController]
    public class DemandsController : ControllerBase
    {
        private readonly IDemandService demandService;
        private readonly IMapper mapper;

        public DemandsController(IDemandService demandService,IMapper mapper)
        {
            this.demandService = demandService;
            this.mapper = mapper;
        }
        // GET: api/Demands
        [HttpGet]
        public async Task<IActionResult> Get(int index, int size)
        { 
            List<Demand> demands = await demandService.QueryPage(index,size);
            var data = mapper.Map<List<DemandView>>(demands);

            var res = new ApiResult<List<DemandView>>(data); 
            return Ok(res);
        }

        // GET: api/Demands/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Demands
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Demands/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
