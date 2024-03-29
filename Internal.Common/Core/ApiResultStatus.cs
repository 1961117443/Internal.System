﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    /// <summary>
    /// API 返回消息数据的状态
    /// </summary>
    public class ApiResultStatus
    {
        /// <summary>
        /// 成功
        /// </summary>
        public static readonly int OK = 0;

        /// <summary>
        /// 返回影响数为0
        /// </summary>
        public static readonly int F10010 = 10010;

    }

    public enum ResultStatus
    {
        OK

    }

    public enum TipMessageEnum
    {
        发布成功,
        发布失败, 
        a0,
        hahaha,
        哎呀
    }
}
