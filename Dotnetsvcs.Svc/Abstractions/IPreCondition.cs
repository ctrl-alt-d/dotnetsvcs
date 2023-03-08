using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IPreCondition<TParms>
    where TParms : IDtoParm
{
    public Task Check(TParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken);
}
