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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Internal.App.Controllers
{
    /// <summary>
    /// 评论管理
    /// </summary>
    [Route("api/comment")]
    [ApiController]
    public class CommentController : BaseController
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public CommentController(ICommentService commentService,IMapper mapper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取评论列表
        /// </summary>
        /// <param name="id">所属内容</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        // GET: api/Comment
        [HttpGet]
        public async Task<IActionResult> Get(string id,int pageIndex)
        {
            var list= await commentService.QueryPageEx(w => w.SubordinateID.Equals(id.toGuid()), pageIndex, 10,e=>e.CommentTime);
            ResultModel<List<CommentViewModel>> res = new ResultModel<List<CommentViewModel>>()
            {
                Data = mapper.Map<List<CommentViewModel>>(list)
            };
            return Ok(res);
        }

        // GET: api/Comment/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        /// <summary>
        /// 发表评论
        /// </summary>
        /// <param name="viewModel">评论内容</param>
        /// <returns></returns>
        // POST: api/Comment
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CommentViewModel viewModel,[FromServices]IAspNetUser user)
        {

            ResultModel<CommentViewModel> resultModel = new ResultModel<CommentViewModel>();

            if (viewModel.SubordinateID.isEmpty())
            {
                resultModel.Message = "发表失败！";
                return Ok(resultModel); 
            }
            var comment = this.mapper.Map<Comment>(viewModel);
            comment.Commentator = user.Name;
            comment.CommentTime = DateTime.Now; 
            var r = await commentService.Add(comment);
            if (r>0)
            {
                resultModel.Message = "发表成功！";
                resultModel.Data = this.mapper.Map<CommentViewModel>(comment);
            }
            return Ok(resultModel);
        }

        //// PUT: api/Comment/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
