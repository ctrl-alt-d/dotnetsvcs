using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using Dotnetsvcs.Svc.CtxWrapperHelpers;
using Dotnetsvcs.Svc.DtoParm;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc;

public abstract class DbOpDelete<T, TParms> : DbOpCUDBase<T, TParms>, IDbOpDelete<T, TParms> where T : class
    where TParms : DtoParmUpdate
{
    protected DbOpDelete(IDbCtxWrapperFactory dbCtxWrapperFactory, IPreConditions<TParms> preConditions, IPostConditions<T, TParms> postConditions) :
        base(dbCtxWrapperFactory, preConditions, postConditions)
    {
    }

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

        var result =
            DbCtxWrapper
            .Set<T>()
            .Where(x => x == entity)
            .Select(projection)
            .First();

        DbCtxWrapper.Remove(entity);

        if (SaveChangesFlag) await DbCtxWrapper.SaveChangesAsync(cancellationToken);

        await PostActions(entity, cancellationToken);

        await CheckPostConditions(entity, parms, cancellationToken);

        return result;
    }

}
