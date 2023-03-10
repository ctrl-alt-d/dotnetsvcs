using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IDbOpDelete<T, TParms> : IDbOpCUDBase<T, TParms>
    where T : class
    where TParms : DtoParmUpdate {
}
