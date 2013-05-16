using System;
using System.Linq.Expressions;

namespace DownMarker.Core
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<TObject>(
           this Expression<Func<TObject, object>> expression)
        {
            Expression body = expression.Body;
            // strip conversion first if necessary, to support value types
            if (body.NodeType == ExpressionType.Convert)
            {
                body = ((UnaryExpression)body).Operand;
            }
            var memberExpression = body as MemberExpression;
            if ((memberExpression == null)
                || (memberExpression.Member.MemberType == System.Reflection.MemberTypes.Field))
            {
                throw new ArgumentException(String.Format(
                      "'{0}' is not a member expression for a property", expression.Body));
            }
            return memberExpression.Member.Name;
        }

    }
}
