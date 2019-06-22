using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    /// <summary>
    /// 令牌 载体 payload
    /// </summary>
    public class TokenModelJwt
    { 
        /// <summary>
        /// Id
        /// </summary>
        public string Uid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 职能
        /// </summary>
        public string Work { get; set; }

    }
}
