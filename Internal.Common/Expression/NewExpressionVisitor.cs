using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Internal.Common
{
    /// <summary>
    /// 统一参数，更改表达式目录树的参数
    /// </summary>
    internal class NewExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression _NewParameter { get; private set; }
        public NewExpressionVisitor(ParameterExpression param)
        {
            this._NewParameter = param;
        }
        public Expression Replace(Expression exp)
        {
            return this.Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            return this._NewParameter;
        }
    }

}
