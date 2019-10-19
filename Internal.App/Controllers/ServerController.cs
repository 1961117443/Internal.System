using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Internal.Common.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Reflection;
using Internal.Common.Core;
using Internal.IService;

namespace Internal.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        public ServerController(IDBSchemaService schemaService, Service.QiniuService qiniuService, IBaseService<Data.ProductDiagram> productDiagramService)
        {
            this.schemaService = schemaService;
            this.qiniuService = qiniuService;
            this.productDiagramService = productDiagramService;
        }
        [HttpGet("api/server/encrypt")]
        public string Encrypt(string input, string key)
        {
            return EncryptHelper.AESEncrypt(input, key);
        }

        [HttpGet("api/server/decrypt")]
        public string Decrypt(string input, string key)
        {
            return EncryptHelper.AESDecrypt(input, key);
        }

        [HttpGet("api/server/datetime")]
        public string GetServerDateTime()
        {
            var time = DateTime.Now - DateTime.Parse("2019-07-01");
            return time.TotalDays.ToString();
        }

        private MethodInfo[] methods = typeof(Math).GetMethods();
        private readonly IDBSchemaService schemaService;
        private readonly Service.QiniuService qiniuService;
        private readonly IBaseService<Data.ProductDiagram> productDiagramService;

        [HttpPost("eval")]
        public object Eval(JObject jObject)
        {  
            ResultModel<double> resultModel = new ResultModel<double>();
            //JArray jArray = new JArray();
            //jArray.Add(new { name= ":h", value=2});
            //jArray.Add(new { name = ":L", value = 8 });   
            //jObject.Add("formula", ":h/2+SQRT(:L+:h)/(8*:h)");
            //jObject.Add("params", jArray);
            //var str = ":h/2+SQRT(L+h)/(8h)";
            DataTable dt = new DataTable();
            var str = jObject.Value<string>("formula");
            var jarr= jObject.Value<JArray>("items");
            foreach (var item in jarr)
            {
                var key = item.Value<string>("name");
                var p = item.Value<string>("value");
                str = str.Replace(key, p);
            }
            try
            {
                foreach (var method in methods)
                {
                    if (str.Contains(method.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        var p = $@"{method.Name}(\([^\)]*\))";
                        Regex regex = new Regex(p, RegexOptions.IgnoreCase);
                        foreach (var item in regex.Matches(str))
                        {
                            var parameter = item.ToString().Replace(method.Name, "", StringComparison.OrdinalIgnoreCase).TrimStart('(').TrimEnd(')');
                            var pts = parameter.Split(',');
                            List<object> objs = new List<object>();
                            foreach (var pt in pts)
                            {
                                objs.Add(dt.Compute(pt, ""));
                            }
                            var v = method.Invoke(null, objs.ToArray());
                            str = str.Replace(item.ToString(), v.ToString()); 
                        }
                    }
                }
                resultModel.Data = double.Parse(dt.Compute(str, "").ToString()); 
            }
            catch (Exception ex)
            {
                resultModel.Message = ex.Message;
                resultModel.Success = false;
            } 

            return Ok(resultModel);
        }


        /// <summary>
        /// 获取表架构,转成json格式
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("GetTableSchema")]
        public async Task<IActionResult> GetTableSchema(string tableName)
        {
           var data=await this.schemaService.DbToCSharp(tableName);
            return Ok(data);
        }

        /// <summary>
        /// 获取表架构,转成json格式
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        [HttpGet("upload")]
        public async Task<IActionResult> UploadImg()
        {
            var list = await productDiagramService.QueryPageAsync(1,5000);
            foreach (var item in list)
            {
                await qiniuService.UploadData(item.ID.toShort(), item.Image);
            } 
            return Ok(new { });
        }
    }
}