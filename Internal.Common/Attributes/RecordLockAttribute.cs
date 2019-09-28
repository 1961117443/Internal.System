using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Attributes
{
    /// <summary>
    /// 检查记录锁定特性
    /// </summary>
    public class RecordLockAttribute : BaseInterceptorAttribute
    {
        public Type EntityType { get; set; }
        public RecordLockAttribute(InterceptorType action) : base(action)
        {
        }
 

        public override void AfterExecute(IInvocation invocation)
        {
            throw new NotImplementedException();
        }

        public override void BeforeExecute(IInvocation invocation)
        {
            throw new NotImplementedException();
        }
    }
}
