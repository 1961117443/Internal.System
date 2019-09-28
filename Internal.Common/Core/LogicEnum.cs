using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Core
{
    public enum LogicEnum
    {
        Equal = 0,
        Like = 1,
        GreaterThan = 2,
        GreaterThanOrEqual = 3,
        LessThan = 4,
        LessThanOrEqual = 5,
        In = 6,
        NotIn = 7,
        LikeLeft = 8,
        LikeRight = 9,
        NoEqual = 10,
        IsNullOrEmpty = 11,
        IsNot = 12,
        NoLike = 13
    }
}
