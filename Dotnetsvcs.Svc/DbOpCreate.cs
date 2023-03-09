using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using Dotnetsvcs.Svc.DtoParm;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc;

public abstract class DbOpCreate<T, TParms> :
    DbOpCUDBase<T, TParms>, // Base
    IDbOpCreate<T, TParms>  // Interface
    where T : class
    where TParms : DtoParmCreate
{
    protected DbOpCreate(
        IDbCtxWrapperFactory dbCtxWrapperFactory, 
        IPreCondition<TParms> preCondition, 
        IPostCondition<T, TParms> postCondition
        ) :
        base(dbCtxWrapperFactory, preCondition, postCondition)
    {
    }

    protected abstract Task<T> CreateEntityFromParms(TParms parms, CancellationToken cancellationToken = default);
    public override async Task<TDtoResult> Do<TDtoResult>(
        TParms parms,
        Expression<Func<T, TDtoResult>> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default)
    {
        if (ctx != null) this.UseDbCtxWrapper(ctx);

        await CheckPreconditions(parms, cancellationToken);

        await PreActions(parms, cancellationToken);

        var entity = await CreateEntityFromParms(parms, cancellationToken);

        await DbCtxWrapper.AddAsync(entity, cancellationToken);

        if (SaveChangesFlag) await DbCtxWrapper.SaveChangesAsync(cancellationToken);

        await PostActions(parms, entity, cancellationToken);

        await CheckPostConditions(entity, parms, cancellationToken);

        var result =
            await
            DbCtxWrapper
            .FirstWithProjectionAsync(
                projection: projection,
                where: x => x == entity);

        return result;
    }

}
