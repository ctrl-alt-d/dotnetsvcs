using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DtoParm;
using Dotnetsvcs.DtoResult;

namespace Dotnetsvcs.Svc.Abstractions.BaseOps;
public interface IDbOpCUDBase<T, TParms> : IDbOpBase
    where T : class
    where TParms : IDtoParm {
    Task<TDtoResult> Do<TDtoResult>(
        TParms parms,
        IProjection<T, TDtoResult> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default) where TDtoResult : class, IDtoResult;
}
