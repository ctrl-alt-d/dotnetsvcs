using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DtoData.Abstractions;
using Dotnetsvcs.DtoParm.Abstractions;

namespace Dotnetsvcs.Svc.Abstractions.BaseOps;
public interface IDbOpCUDBase<T, TParms> : IDbOpBase
    where T : class
    where TParms : IDtoParm {
    Task<TDtoData> Do<TDtoData>(
        TParms parms,
        IProjection<T, TDtoData> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default) where TDtoData : class, IDtoData;
}
