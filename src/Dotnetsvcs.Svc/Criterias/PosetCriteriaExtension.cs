using Dotnetsvcs.DtoParm.Abstractions.Criteria;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Criterias;

/// <summary>
/// Poset is for Partially ordered set (Dates and Numbers)
/// </summary>
public static class PosetCriteriaExtension
{
    public static Expression<Func<T, bool>> WhereExpression<T, TProp>( this IntCriteriaDto criteria, Expression<Func<T, TProp>> propertyExpression)
     where TProp: IEquatable<TProp>, IComparable<TProp>
    {
        var op = criteria.Operation;

        var expression = (MemberExpression)propertyExpression.Body;
        var field = expression.Member.Name;

        Expression<Func<T, bool>> e = op switch
        {
            // 
            IntCriteriaDto.OperationType.GreatherThan =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} > @0", criteria.Value1),

            // 
            IntCriteriaDto.OperationType.GreatherEqualThan =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} >= @0", criteria.Value1),

            // 
            IntCriteriaDto.OperationType.LessThan =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} < @0", criteria.Value1),

            // 
            IntCriteriaDto.OperationType.LessEqualThan =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} <= @0", criteria.Value1),

            // 
            IntCriteriaDto.OperationType.Empty =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} == null"),

            // 
            IntCriteriaDto.OperationType.NotEmpty =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} != null"),

            //
            IntCriteriaDto.OperationType.InRange =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} >= @0 && {field} <= @1", criteria.Value1, criteria.Value2),

            //
            IntCriteriaDto.OperationType.NotInRange =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} < @0 || {field} > @1", criteria.Value1, criteria.Value2),

            //
            IntCriteriaDto.OperationType.NotEquals =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} != @0 ", criteria.Value1),

            //
            _ =>
                DynamicExpressionParser
                .ParseLambda<T, bool>(new ParsingConfig(), true, $"{field} == @0", criteria.Value1),
        };

        return e;
    }
}
