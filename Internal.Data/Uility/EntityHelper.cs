using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Internal.Data.Uility
{
    /// <summary>
    /// 实体帮助类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityHelper<T>
    {
        static EntityHelper()
        {
           
        }
        /// <summary>
        /// 所有的成员属性
        /// </summary>
        public static PropertyInfo[] PublicInstance = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        private static PropertyInfo[] dbFields;

        /// <summary>
        /// 映射数据库的字段
        /// </summary>
        public static PropertyInfo[] DbFields
        {
            get
            {
                if (dbFields==null || dbFields.Length==0)
                {
                    List<PropertyInfo> list = new List<PropertyInfo>();
                    foreach (var property in PublicInstance)
                    {
                        if (!property.CanWrite || property.PropertyType.IsClass || property.SetMethod.IsVirtual)
                        {
                            continue;    
                        }
                        list.Add(property);
                        dbFields = list.ToArray();
                    } 
                }
                return dbFields;
            }
        }
    }

    public class EntityHelper<TEntity, TDto>
    {
        
    }
}
