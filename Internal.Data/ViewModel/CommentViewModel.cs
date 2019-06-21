using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.ViewModel
{
    public class CommentViewModel
    { 
        /// <summary>
        /// 所属id
        /// 一般根据这个id找出所有的评论
        /// </summary>
        public Guid SubordinateID { get; set; } 
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论人
        /// </summary>
        public string Commentator { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime CommentTime { get; set; }
    }
}
