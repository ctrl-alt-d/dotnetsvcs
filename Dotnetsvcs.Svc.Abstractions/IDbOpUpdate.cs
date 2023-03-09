using Dotnetsvcs.DtoParm;
using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IDbOpUpdate<T, TParms> : IDbOpCUDBase<T, TParms>
    where T : class
    where TParms : DtoParmUpdate {
}