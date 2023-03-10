using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DtoParm.Abstractions;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IPreCondition<TParms>: IDisposable
    where TParms : IDtoParm {
    public Task Check(TParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken);
}
