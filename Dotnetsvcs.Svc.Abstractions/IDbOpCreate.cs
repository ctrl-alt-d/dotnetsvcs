using Dotnetsvcs.DtoParm;
using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IDbOpCreate<T, TParms> : IDbOpCUDBase<T, TParms>
    where T : class
    where TParms : DtoParmCreate {
}