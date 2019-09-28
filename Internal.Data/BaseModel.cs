using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Data
{
    /// <summary>
    /// 所有实体的基类
    /// </summary>
    /// <typeparam name="T">主键类型</typeparam>
    public abstract class BaseModel<T>:Internal.Common.Data.BaseModel<T>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        [FreeSql.DataAnnotations.Column(IsPrimary = true)]
        public override T ID { get; set; }
    }
}
