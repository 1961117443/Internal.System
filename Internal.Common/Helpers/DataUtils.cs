using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Helpers
{
    public static class DataUtils
    {
        #region string
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
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string val)
        {
            return string.IsNullOrEmpty(val) || string.IsNullOrWhiteSpace(val);
        }

        #endregion
        #region Guid
        /// <summary>
        /// 判断guid是否为空
        /// </summary>
        /// <param name="val">guid值</param>
        /// <returns></returns>
        public static bool isEmpty(this Guid val)
        {
            return val.Equals(Guid.Empty);
        }

        /// <summary>
        /// 去掉guid中的-
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string toShort(this Guid guid)
        {
            return guid.ToString().Replace("-", "");
        }
        #endregion

        public static void Copy<TSource,TTarget>(TSource source, TTarget target)
        {

        }
    }
}
