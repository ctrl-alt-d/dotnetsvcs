using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DtoData.Abstractions;
using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.Abstractions;
public interface IDbOpRetrieve<T, TParms> : IDbOpBase
    where T : class
    where TParms : IDtoParmRetrieve {
    Task<DtoDataRetrieve<TDtoData>> Do<TDtoData>(
        TParms parms,
        IProjection<T, TDtoData> projection,
        IDbCtxWrapper? ctx = null,
        CancellationToken cancellationToken = default) where TDtoData : class, IDtoData;
}
