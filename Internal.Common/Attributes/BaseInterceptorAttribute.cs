using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BaseInterceptorAttribute:Attribute
    {
        public int Order { get; set; }
        public InterceptorType Action { get; set; }
        public BaseInterceptorAttribute(InterceptorType action)
        {
            Action = action;
        }
        public virtual void Execute(IInvocation invocation)
        {
            switch (Action)
            {
                case InterceptorType.None:
                    break;
                case InterceptorType.Before:
                    BeforeExecute(invocation);
                    break;
                case InterceptorType.After:
                    AfterExecute(invocation);
                    break;
                default:
                    break;
            }

        }

        public abstract void AfterExecute(IInvocation invocation);
        public abstract void BeforeExecute(IInvocation invocation);
    }

    [Flags]
    public enum InterceptorType
    {
        None =1,
        Before =2,
        After =4 
    }
}
