using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Criterias;

public static class AndCriterias
{
    public static Expression<Func<T,bool>> And<T>(params Expression<Func<T, bool>>[] expressions)
    {
        var conditiontxt = expressions.Select((_, i) => $"@{i}(it)");
        var wheretxt = string.Join(" and ", conditiontxt);
        return 
            DynamicExpressionParser
            .ParseLambda<T, bool>(new ParsingConfig(), true, wheretxt, expressions);
    }
}
