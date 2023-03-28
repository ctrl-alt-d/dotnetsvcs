using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using Dotnetsvcs.Svc.CtxWrapperHelpers;

namespace Dotnetsvcs.Svc;

public abstract class DbOpDelete<T, TParms> : DbOpCUDBase<T, TParms>, IDbOpDelete<T, TParms> where T : class
    where TParms : DtoParmDelete {
    protected DbOpDelete(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        IPreCondition<TParms> preCondition,
        IPostCondition<T, TParms> postCondition,
        IFilter<T> filter
        ) :
        base(dbCtxWrapperFactory, preCondition, postCondition, filter) {
    }

    public override async Task<TDtoData> Do<TDtoData>(
        TParms parms,
        IProjection<T, TDtoData> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default) {
        if (ctx != null) this.UseDbCtxWrapper(ctx);

        await CheckPreconditions(parms, cancellationToken);

        await PreActions(parms, cancellationToken);

        var entity = await DbCtxWrapper.FindOrException<T>(parms.KeyValues);

        var result =
            await
            DbCtxWrapper
            .FirstWithProjectionAsync(
                where: x => x == entity,
                filter: await Filter.GetFilter(DbCtxWrapper),
                projection: await projection.GetToDtoData(DbCtxWrapper)
            );

        DbCtxWrapper.Remove(entity);

        if (SaveChangesFlag) await DbCtxWrapper.SaveChangesAsync(cancellationToken);

        await PostActions(parms, entity, cancellationToken);

        await CheckPostConditions(entity, parms, cancellationToken);

        return result;
    }

}
