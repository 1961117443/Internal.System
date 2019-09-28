using Castle.DynamicProxy;
using Internal.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Internal.IService.AOP
{
    /// <summary>
    /// 日志锁定拦截器
    /// </summary>
    public class ServiceInterceptorAOP : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            try
            {
                //记录被拦截方法信息的日志信息
                var dataIntercept = $"{DateTime.Now.ToString("yyyyMMddHHmmss")} " +
                    $"当前执行方法：{ invocation.Method.Name} " +
                    $"参数是： {string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray())} \r\n";

                var attrs = invocation.Method.GetCustomAttributes(typeof(BaseInterceptorAttribute), false).OfType<BaseInterceptorAttribute>();

                foreach (var attr in attrs.Where(a => (a.Action & InterceptorType.Before) == InterceptorType.Before).OrderBy(a => a.Order))
                {
                    attr.Execute(invocation);
                }


                //在被拦截的方法执行完毕后 继续执行当前方法
                invocation.Proceed();

                foreach (var attr in attrs.Where(a => (a.Action & InterceptorType.After) == InterceptorType.After).OrderBy(a => a.Order))
                {
                    attr.Execute(invocation);
                }

                dataIntercept += ($"被拦截方法执行完毕，返回结果：{invocation.ReturnValue}");
            }
            catch (Exception ex)
            {
                invocation.ReturnValue = null;
            } 
        }
    }
}
