﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Data
{
    /// <summary>
    /// 所有实体的基类
    /// </summary>
    /// <typeparam name="T">主键类型</typeparam>
    public abstract class BaseModel<T>
    {
       public virtual T ID { get; set; }
    }
}
