using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DtoData.Abstractions;
using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Abstractions;
public interface IProjection<T, TDtoData> : IsProjection, IDisposable
    where T : class
    where TDtoData : IDtoData {
    Expression<Func<T, TDtoData>> GetToDtoData(IDbCtxWrapper dbCtxWrapper);

}