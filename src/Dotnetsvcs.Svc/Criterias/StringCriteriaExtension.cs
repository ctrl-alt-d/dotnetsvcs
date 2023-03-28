using Dotnetsvcs.DtoParm.Abstractions.Criteria;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Criterias;

public static class StringCriteriaExtension
{
    public static Expression<Func<T, bool>> WhereExpression<T>( this StringCriteriaDto criteria, Expression<Func<T, string?>> propertyExpression)
    {
        var pattern = criteria.Pattern;
        var op = criteria.Operation;

        var expression = (MemberExpression)propertyExpression.Body;
        var field = expression.Member.Name;

        Expression<Func<T, bool>> e = op switch
        {
            // Contains
            StringCriteriaDto.OperationType.Contains =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} != null && {field}.Contains(@0)", pattern),

            // Empty
            StringCriteriaDto.OperationType.Empty =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"string.IsNullOrWhiteSpace({field})"),

            // StartsWith
            _ =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} != null && {field}.StartsWith(@0)", pattern)
        };

        return e;
    }
}
