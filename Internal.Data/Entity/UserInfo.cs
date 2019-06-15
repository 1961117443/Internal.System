using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data.Entity
{
    public class UserInfo
    {
        public Guid ID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
    }
}
