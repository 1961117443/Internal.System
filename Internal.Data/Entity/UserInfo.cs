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
        [SqlSugar.SugarColumn(ColumnName = "Name")]
        public string UserName { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        [SqlSugar.SugarColumn(ColumnName = "Code")]
        public string UserCode { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsDisable { get; set; }
    }
}
