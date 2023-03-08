using Dotnetsvcs.Svc.Abstractions.BaseOps;
using Dotnetsvcs.Svc.DtoParm;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IDbOpCreate<T, TParms> : IDbOpCUDBase<T, TParms>
    where T : class
    where TParms : DtoParmCreate
{
}