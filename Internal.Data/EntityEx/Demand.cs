﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Internal.Data.Entity
{
    /// <summary>
    /// Demand虚拟属性
    /// </summary>
    public partial class Demand
    {
        [SugarColumn(IsIgnore = true)]
        [ForeignKey("CustomerID")]
        public virtual Customer Customer { get; set; }
    }
}
