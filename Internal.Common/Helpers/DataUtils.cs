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

        #region Type
        /// <summary>
        /// 获取类型简称
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ShortName(this Type type)
        {
            var name = type.Name;
            switch (name)
            {
                case "Int32":
                case "Int64":
                    name = "int";
                    break;
                case "String":
                    name = "string";
                    break;
                case "Boolean":
                    name = "bool";
                    break;
                default: 
                    break;
            }
            return name;
        }
        public static bool Nullable(this Type type)
        {
            var name = type.ShortName(); 
            switch (name)
            {
                case "string":
                case "Guid":
                    return false; 
                default:
                    return true;
            } 
        } 
        #endregion

        #region StringBuilder
        /// <summary>
        /// 插入的时候加一个tab
        /// </summary>
        /// <param name="stringBuilder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringBuilder AppendTabLine(this StringBuilder stringBuilder,string value)
        {
            return stringBuilder.AppendLine($"\t{value}");
        }
        #endregion
    }
}
