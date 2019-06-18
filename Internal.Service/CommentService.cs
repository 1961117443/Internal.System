using Internal.Data.Entity;
using Internal.IRepository;
using Internal.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Service
{
    public class CommentService : BaseService<Comment>, ICommentService
    {
        private readonly ICommentRepository commentRepository;

        protected override IBaseRepository<Comment> Repository => commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
    }
}
