﻿using Dotnetsvcs.DtoParm;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.BaseOps;
using Dotnetsvcs.Svc.CtxWrapperHelpers;

namespace Dotnetsvcs.Svc;

public abstract class DbOpUpdate<T, TParms> : DbOpCUDBase<T, TParms>, IDbOpUpdate<T, TParms> where T : class
    where TParms : DtoParmUpdate {
    protected DbOpUpdate(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        IPreCondition<TParms> preCondition,
        IPostCondition<T, TParms> postCondition
        ) :
        base(dbCtxWrapperFactory, preCondition, postCondition) {
    }

    protected abstract Task<T> UpdateEntityFromParms(TParms parms, T entity, CancellationToken cancellationToken = default);

    public override async Task<TDtoResult> Do<TDtoResult>(
        TParms parms,
        IProjection<T, TDtoResult> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default) {
        if (ctx != null) this.UseDbCtxWrapper(ctx);

        await CheckPreconditions(parms, cancellationToken);

        await PreActions(parms, cancellationToken);

        var entity = await DbCtxWrapper.FindOrException<T>(parms.keyValues);

        await UpdateEntityFromParms(parms, entity, cancellationToken);

        if (SaveChangesFlag) await DbCtxWrapper.SaveChangesAsync(cancellationToken);

        await PostActions(parms, entity, cancellationToken);

        await CheckPostConditions(entity, parms, cancellationToken);

        var result =
            await
            DbCtxWrapper
            .FirstWithProjectionAsync(
                projection: projection.GetToDtoResult(DbCtxWrapper),
                where: x => x == entity);

        return result;
    }

}
