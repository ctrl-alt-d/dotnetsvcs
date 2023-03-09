using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DtoResult;
using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Abstractions;
public interface IProjection<T, TDtoResult> : IsProjection
    where T : class
    where TDtoResult : IDtoResult {
    Expression<Func<T, TDtoResult>> GetToDtoResult(IDbCtxWrapper dbCtxWrapper);

}