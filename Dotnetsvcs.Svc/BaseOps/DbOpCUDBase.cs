using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Abstractions.BaseOps;
using Dotnetsvcs.Svc.DtoParm;
using Dotnetsvcs.Svc.DtoResult;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.BaseOps;

public abstract class DbOpCUDBase<T, TParms> : DbOpBase, IDbOpCUDBase<T, TParms> where TParms : IDtoParm
    where T : class
{
    protected virtual IPreConditions<TParms> PreConditions { get; }
    protected virtual IPostConditions<T, TParms> PostConditions { get; }

    protected DbOpCUDBase(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        IPreConditions<TParms> preConditions,
        IPostConditions<T, TParms> postConditions
        ) : base(dbCtxWrapperFactory)
    {
        PreConditions = preConditions;
        PostConditions = postConditions;
    }

    protected async Task CheckPreconditions(TParms parms, CancellationToken cancellationToken = default)
    {
        foreach (var preCondition in PreConditions)
        {
            await preCondition.Check(parms, DbCtxWrapper, cancellationToken);
        }
    }

    protected abstract Task PreActions(TParms parms, CancellationToken cancellationToken = default);

    protected abstract Task PostActions(T entity, CancellationToken cancellationToken = default);

    protected async Task CheckPostConditions(T entity, TParms parms, CancellationToken cancellationToken = default)
    {
        foreach (var postCondition in PostConditions)
        {
            await postCondition.Check(entity, parms, DbCtxWrapper, cancellationToken);
        }
    }

    public abstract Task<TDtoResult> Do<TDtoResult>(
        TParms parms,
        Expression<Func<T, TDtoResult>> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default)
        where TDtoResult : IDtoResult;

    protected bool SaveChangesFlag { get; set; } = true;
}