using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IPostCondition<T, TParms>
    where TParms : IDtoParm
    where T : class
{
    public Task Check(T entity, TParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken);
}
