using Dotnetsvcs.DtoParm;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using Dotnetsvcs.Svc.CtxWrapperHelpers;

namespace Dotnetsvcs.Svc;

public abstract class DbOpDelete<T, TParms> : DbOpCUDBase<T, TParms>, IDbOpDelete<T, TParms> where T : class
    where TParms : DtoParmUpdate {
    protected DbOpDelete(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        IPreCondition<TParms> preCondition,
        IPostCondition<T, TParms> postCondition
        ) :
        base(dbCtxWrapperFactory, preCondition, postCondition) {
    }

    public override async Task<TDtoResult> Do<TDtoResult>(
        TParms parms,
        IProjection<T, TDtoResult> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default) {
        if (ctx != null) this.UseDbCtxWrapper(ctx);

        await CheckPreconditions(parms, cancellationToken);

        await PreActions(parms, cancellationToken);

        var entity = await DbCtxWrapper.FindOrException<T>(parms.keyValues);

        var result =
            await
            DbCtxWrapper
            .FirstWithProjectionAsync(
                projection: projection.GetToDtoResult(DbCtxWrapper),
                where: x => x == entity);

        DbCtxWrapper.Remove(entity);

        if (SaveChangesFlag) await DbCtxWrapper.SaveChangesAsync(cancellationToken);

        await PostActions(parms, entity, cancellationToken);

        await CheckPostConditions(entity, parms, cancellationToken);

        return result;
    }

}
