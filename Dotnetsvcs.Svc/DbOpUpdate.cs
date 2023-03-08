using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using Dotnetsvcs.Svc.CtxWrapperHelpers;
using Dotnetsvcs.Svc.DtoParm;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc;

public abstract class DbOpUpdate<T, TParms> : DbOpCUDBase<T, TParms>, IDbOpUpdate<T, TParms> where T : class
    where TParms : DtoParmUpdate
{
    protected DbOpUpdate(IDbCtxWrapperFactory dbCtxWrapperFactory, IPreConditions<TParms> preConditions, IPostConditions<T, TParms> postConditions) :
        base(dbCtxWrapperFactory, preConditions, postConditions)
    {
    }

    protected abstract Task<T> UpdateEntityFromParms(TParms parms, T entity, CancellationToken cancellationToken = default);

    public override async Task<TDtoResult> Do<TDtoResult>(
        TParms parms,
        Expression<Func<T, TDtoResult>> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default)
    {
        if (ctx != null) this.UseDbCtxWrapper(ctx);
        await CheckPreconditions(parms, cancellationToken);

        await PreActions(parms, cancellationToken);

        var entity = await DbCtxWrapper.FindOrException<T>(parms.keyValues, cancellationToken: cancellationToken);

        await UpdateEntityFromParms(parms, entity, cancellationToken);

        if (SaveChangesFlag) await DbCtxWrapper.SaveChangesAsync(cancellationToken);

        await PostActions(entity, cancellationToken);

        await CheckPostConditions(entity, parms, cancellationToken);

        var result =
            DbCtxWrapper
            .Set<T>()
            .Where(x => x == entity)
            .Select(projection)
            .First();

        return result;
    }

}
