using Internal.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Internal.Data.Uility
{
    public static class ExpressionHelper
    {

        #region 缓存
        private static Dictionary<string, MemberExpression> memberExpressionCache = new Dictionary<string, MemberExpression>();
        #endregion
        #region 锁 locker
        private static readonly object member_expression_locker = new object();
        #endregion

        #region 字段 变量
        private static readonly string EntityAssemblyName = "Internal.Data.Entity"; 
        #endregion

        /// <summary>
        /// 比较两个对象的值，如果不一样返回表达式
        /// </summary>
        /// <typeparam name="TEntity">entity模型 最终返回的类型</typeparam>
        /// <typeparam name="TView">view模型 取数的数据源</typeparam>
        /// <param name="entity">实体对象</param>
        /// <param name="dto">dto对象</param>
        public static Expression<Func<TEntity, TEntity>> UpdateExpression<TView,TEntity>(this TEntity entity, TView dto)
        {
            ParameterExpression parameterExpression = Expression.Parameter(typeof(TEntity), "a");

            // var copy = entity.Clone();

            var entityPropties = EntityHelper<TEntity>.PublicInstance;
            //var viewPropties = EntityHelper<TView>.PublicInstance;

            List<MemberBinding> bindings = new List<MemberBinding>();

            foreach (var viewProp in EntityHelper<TView>.PublicInstance)
            {
                var entityProp = entityPropties.FirstOrDefault(w => w.Name == viewProp.Name);
                if (entityProp==null)
                {
                    continue;
                } 
                
                object entityValue = entityProp.GetValue(entity, null);
                object dtoValue = viewProp.GetValue(dto, null);
                
                
                if (entityValue == null && dtoValue == null)
                {
                    continue;
                }
                try
                {
                    if (dtoValue!=null && viewProp.PropertyType!=entityProp.PropertyType)
                    {
                        if (entityProp.PropertyType== typeof(Guid))
                        {
                            dtoValue = Guid.Parse(dtoValue.ToString());
                        }
                        else
                        {
                            dtoValue = Convert.ChangeType(dtoValue, entityProp.PropertyType);
                        } 
                    } 
                }
                catch (Exception ex)
                {
                    continue;
                }
                if ((entityValue == null && dtoValue != null) || (entityValue != null && dtoValue == null) || (entityValue != dtoValue && !entityValue.Equals(dtoValue)))
                {
                    ConstantExpression constant = Expression.Constant(dtoValue,entityProp.PropertyType);
                    bindings.Add(Expression.Bind(entityProp, constant));
                }
            }

            Expression<Func<TEntity, TEntity>> expression = Expression.Lambda<Func<TEntity, TEntity>>(Expression.MemberInit(Expression.New(typeof(TEntity)), bindings.ToArray()), parameterExpression);

            return expression;
        }

        private static Expression<Func<T, bool>> ToExpression<T>(ParameterExpression parameterExpression, Expression member, QueryParam queryParam)
        {
            if (parameterExpression==null)
            {
                parameterExpression = Expression.Parameter(typeof(T), "a");
            }
            Type regionType = member.Type;
            object obj = null;
            Type valueType = null;
            if (regionType.IsNullableType())
            {
                valueType = regionType.GetGenericArguments().First();
            }
            else
            {
                valueType = regionType; 
            }
            obj = Convert.ChangeType(queryParam.Value, valueType);
            ConstantExpression constant = Expression.Constant(obj, valueType);
            switch (queryParam.Logic)
            {
                //等于
                case Common.Core.LogicEnum.Equal:
                    return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameterExpression);
                case Common.Core.LogicEnum.Like:
                    {
                        MethodInfo method = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameterExpression);
                    }
                //右包含
                case Common.Core.LogicEnum.LikeLeft:
                    {
                        MethodInfo method = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameterExpression);
                    }
                //左包含
                case Common.Core.LogicEnum.LikeRight:
                    {
                        MethodInfo method = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                        return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameterExpression);
                    }
                case Common.Core.LogicEnum.GreaterThan:
                    {
                        //var value = Convert.ChangeType(queryParam.Value, typeof(DateTime));
                        //constant = Expression.Constant(value, member.Type);
                         var exp = Expression.Convert(constant, member.Type);
                        MethodInfo method = typeof(DateTime).GetMethod("op_GreaterThan");
                        return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, exp, false, method), parameterExpression);
                    }
                case Common.Core.LogicEnum.GreaterThanOrEqual:
                    break;
                case Common.Core.LogicEnum.LessThan:
                    break;
                case Common.Core.LogicEnum.LessThanOrEqual:
                    break;
                case Common.Core.LogicEnum.In:
                    break;
                case Common.Core.LogicEnum.NotIn:
                    break;
                case Common.Core.LogicEnum.NoEqual:
                    break;
                case Common.Core.LogicEnum.IsNullOrEmpty:
                    break;
                case Common.Core.LogicEnum.IsNot:
                    break;
                case Common.Core.LogicEnum.NoLike:
                    break;
            }
            return null;
        }

        public static Expression<Func<T, bool>> ToExpression<T>(this QueryParam queryParam)
        {
            var key = $"{typeof(T).FullName}.{queryParam.Field}";
            ParameterExpression parameterExpression = Expression.Parameter(typeof(T), "a");
            if (!memberExpressionCache.ContainsKey(key))
            {
                lock (member_expression_locker)
                {
                    if (!memberExpressionCache.ContainsKey(key))
                    {
                        var exp = GetMemberExpression(parameterExpression, queryParam.Field);
                        memberExpressionCache.Add(key, exp);
                    }
                }
            }
            Expression member = memberExpressionCache[key];
            if (member==null)
            {
                return null;
            }
            return ToExpression<T>(parameterExpression, member, queryParam);
            //ConstantExpression constant = Expression.Constant(queryParam.Value, typeof(string));
            //switch (queryParam.Logic)
            //{
            //    //等于
            //    case Common.Core.LogicEnum.Equal:
            //        return Expression.Lambda<Func<T, bool>>(Expression.Equal(member, constant), parameterExpression);
            //    case Common.Core.LogicEnum.Like:
            //        {
            //            MethodInfo method = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
            //            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameterExpression);
            //        }
            //    //右包含
            //    case Common.Core.LogicEnum.LikeLeft:
            //        {
            //            MethodInfo method = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
            //            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameterExpression);
            //        }
            //    //左包含
            //    case Common.Core.LogicEnum.LikeRight: 
            //        {  
            //            MethodInfo method = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) }); 
            //            return Expression.Lambda<Func<T, bool>>(Expression.Call(member, method, constant), parameterExpression);
            //        }
            //    case Common.Core.LogicEnum.GreaterThan:
            //        {
            //            var value = Convert.ChangeType(queryParam.Value, typeof(DateTime));
            //            constant = Expression.Constant(value, member.Type);
            //           // var exp = Expression.Convert(constant, member.Type);
            //            MethodInfo method = typeof(DateTime).GetMethod("op_GreaterThan");
            //            return Expression.Lambda<Func<T, bool>>(Expression.GreaterThan(member, constant, false, method),parameterExpression); 
            //        } 
            //    case Common.Core.LogicEnum.GreaterThanOrEqual:
            //        break;
            //    case Common.Core.LogicEnum.LessThan:
            //        break;
            //    case Common.Core.LogicEnum.LessThanOrEqual:
            //        break;
            //    case Common.Core.LogicEnum.In:
            //        break;
            //    case Common.Core.LogicEnum.NotIn:
            //        break;
            //    case Common.Core.LogicEnum.NoEqual:
            //        break;
            //    case Common.Core.LogicEnum.IsNullOrEmpty:
            //        break;
            //    case Common.Core.LogicEnum.IsNot:
            //        break;
            //    case Common.Core.LogicEnum.NoLike:
            //        break; 
            //} 
            //return null;
        }

        /// <summary>
        /// 根据字段名获取表达式目录树
        /// parameterExpression.Type = Demand
        /// fieldName="ClientFile.ClientName" ： a=>a.ClientFile.ClientName
        /// </summary>
        /// <param name="parameterExpression"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static MemberExpression GetMemberExpression(Expression parameterExpression, string fieldName)
        {
            int index = fieldName.IndexOf('.');
            string f = index >= 0 ? fieldName.Substring(0, index) : fieldName;
            fieldName = fieldName.Substring(Math.Min(f.Length + 1, fieldName.Length));

            var propertyInfo = parameterExpression.Type.GetProperty(f);
            if (propertyInfo == null)
            {
                return null;
            }
            var expression = Expression.Property(parameterExpression, propertyInfo);
            if (index >= 0)
            { 
                return GetMemberExpression(expression, fieldName);
            }
            return expression;
        }
        /// <summary>
        /// 根据字段名获取表达式目录树
        /// 有缓存
        /// fullName="Demand.ClientFile.ClientName" ： a=>a.ClientFile.ClientName
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static MemberExpression GetMemberExpression(string fullName)
        {
            if (!memberExpressionCache.ContainsKey(fullName))
            {
                lock (member_expression_locker)
                {
                    if (!memberExpressionCache.ContainsKey(fullName))
                    {
                        int index = fullName.IndexOf('.');
                        MemberExpression expression = null;
                        if (index>=0)
                        {
                            string className = fullName.Substring(0, index);
                            string fieldName = fullName.Substring(index + 1);
                            var type = Type.GetType($"{EntityAssemblyName}.{className}");
                            if (type!=null)
                            {
                                ParameterExpression parameterExpression = Expression.Parameter(type, "a");
                                expression = GetMemberExpression(parameterExpression, fieldName);
                            } 
                        }
                        memberExpressionCache.Add(fullName, expression);
                    }
                }
            }
            return memberExpressionCache[fullName];
        }

    }
}
