using Dotnetsvcs.DtoData.Abstractions;
using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc;

public abstract class DbOpRetrieve<T, TParms> : 
    DbOpBase, 
    IDbOpRetrieve<T, TParms>     
    where T : class
    where TParms : IDtoParmRetrieve {

    public DbOpRetrieve(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        IFilter<T> filter
        )
        : base(dbCtxWrapperFactory) {
        Filter = filter;
    }

    protected virtual IFilter<T> Filter { get; }

    protected abstract Task<Expression<Func<T, bool>>> GetWhereFromParms(TParms parms);

    public async Task<DtoDataRetrieve<TDtoData>> Do<TDtoData>(
        TParms parms, 
        IProjection<T, TDtoData> projection,
        IDbCtxWrapper? ctx, 
        CancellationToken cancellationToken)
        where TDtoData : class, IDtoData
        {

        if (ctx != null) this.UseDbCtxWrapper(ctx);

        var filterExpression = await Filter.GetFilter(DbCtxWrapper);
        var where = await GetWhereFromParms(parms);
        var projectionExpression = await projection.GetToDtoData(DbCtxWrapper);

        var query =
            DbCtxWrapper
            .SetAsNoTracking<T>()
            .Where(filterExpression)
            .Where(where)
            .Select(projectionExpression);

        var items =
            query
            .Skip(parms.Page * parms.ItemsPerPage)
            .Take(parms.ItemsPerPage)
            .ToList();

        var totalCount =
            parms.TotalCountRequired ?
            query.Count():
            (int?) null;                

        var result = new DtoDataRetrieve<TDtoData>() {
            TotalCount = totalCount,
            Items = items
        };

        return result;
    }
}
