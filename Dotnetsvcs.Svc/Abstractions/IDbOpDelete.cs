using Dotnetsvcs.Svc.Abstractions.BaseOps;
using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IDbOpDelete<T, TParms> : IDbOpCUDBase<T, TParms>
    where T : class
    where TParms : DtoParmUpdate
{
}
