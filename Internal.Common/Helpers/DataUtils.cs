using Internal.Common.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Internal.Common.Helpers
{
    public static class DataUtils
    {
        private static Dictionary<string, MethodInfo> logicMehtodCache = new Dictionary<string, MethodInfo>();
        private static readonly object logic_mehtod_locker = new object();
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

        /// <summary>
        /// 获取查询逻辑对应的C#方法
        /// </summary>
        /// <param name="logicEnum"></param>
        /// <returns></returns>
        public static MethodInfo ToMethod(this LogicEnum logicEnum)
        {
            var key = logicEnum.ToString();
            MethodInfo method = null;
            if (!logicMehtodCache.ContainsKey(key))
            {
                lock (logic_mehtod_locker)
                {
                    if (!logicMehtodCache.ContainsKey(key))
                    {
                        switch (logicEnum)
                        {
                            case LogicEnum.Like:
                            case LogicEnum.NoLike:
                                method = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                                break;
                            case LogicEnum.LikeLeft:
                                method = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                                break;
                            case LogicEnum.LikeRight:
                                method = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                                break;
                            case LogicEnum.IsNullOrEmpty: 
                            default:
                                break;
                                
                        }
                        if (method ==null)
                        {
                            throw new Exception("没找到合适的方法");
                        }
                        logicMehtodCache.Add(key, method);
                    }
                } 
            }
            return logicMehtodCache[key];
        }

        /// <summary>
        /// 通用的类型转换方法
        /// </summary>
        /// <returns></returns>
        public static object ChangeType(object value, Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }
    }
}
