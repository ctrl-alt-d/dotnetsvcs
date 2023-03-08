using Dotnetsvcs.Svc.BaseOps;
using Dotnetsvcs.Svc.DtoParm;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc;

public abstract class DbOpRetrieve<T, TParms, TProjection> : DbOpBase
    where T : class
    where TParms : DtoParmRetrieve
    where TProjection : class
{

    public DbOpRetrieve(IDbCtxWrapperFactory dbCtxWrapperFactory)
        : base(dbCtxWrapperFactory)
    {
    }

    protected abstract Task<Expression<Func<T, bool>>> GetWhereExpression(TParms parms);

    protected abstract Task<Expression<Func<T, TProjection>>> GetProjectionExpression(TParms parms);

    public async Task<List<TProjection>> Do(TParms parms)
    {
        var where = await GetWhereExpression(parms);

        var projection = await GetProjectionExpression(parms);

        var result =
            DbCtxWrapper
            .Set<T>()
            .Where(where)
            .Select(projection)
            .ToList();

        return result;

    }

}
