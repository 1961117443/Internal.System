using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Helpers
{
    public static class DataUtils
    {
        /// <summary>
        /// 把字符串转换成guid类型
        /// </summary>
        /// <param name="val">字符串</param>
        /// <returns></returns>
        public static Guid toGuid(this string val)
        {
            Guid gid = Guid.Empty;
            Guid.TryParse(val, out gid);
            return gid;
        }

        /// <summary>
        /// 判断guid是否为空
        /// </summary>
        /// <param name="val">guid值</param>
        /// <returns></returns>
        public static bool isEmpty(this Guid val)
        {
            return val.Equals(Guid.Empty); 
        }

        public static void Copy<TSource,TTarget>(TSource source, TTarget target)
        {

        }
    }
}
