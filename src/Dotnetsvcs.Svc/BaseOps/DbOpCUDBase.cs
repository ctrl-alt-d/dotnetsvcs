using Dotnetsvcs.DtoData.Abstractions;
using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.BaseOps;

public abstract class DbOpCUDBase<T, TParms> : DbOpBase, IDbOpCUDBase<T, TParms> where TParms : IDtoParm
    where T : class {
    protected virtual IPreCondition<TParms> PreCondition { get; }
    protected virtual IPostCondition<T, TParms> PostCondition { get; }

    protected DbOpCUDBase(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        IPreCondition<TParms> preCondition,
        IPostCondition<T, TParms> postCondition
        ) : base(dbCtxWrapperFactory) {
        PreCondition = preCondition;
        PostCondition = postCondition;
    }

    protected async Task CheckPreconditions(TParms parms, CancellationToken cancellationToken = default) {
        await PreCondition.Check(parms, DbCtxWrapper, cancellationToken);
    }

    protected abstract Task PreActions(TParms parms, CancellationToken cancellationToken = default);

    protected abstract Task PostActions(TParms parms, T entity, CancellationToken cancellationToken = default);

    protected async Task CheckPostConditions(T entity, TParms parms, CancellationToken cancellationToken = default) {
        await PostCondition.Check(entity, parms, DbCtxWrapper, cancellationToken);
    }

    public abstract Task<TDtoData> Do<TDtoData>(
        TParms parms,
        IProjection<T, TDtoData> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default)
        where TDtoData : class, IDtoData;

    protected bool SaveChangesFlag { get; set; } = true;

    public override void Dispose() {
        base.Dispose();
        PreCondition.Dispose();
        PostCondition.Dispose();
    }

}