using Dotnetsvcs.Svc.DtoParm;
using Dotnetsvcs.Svc.DtoResult;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Abstractions.BaseOps
{
    public interface IDbOpCUDBase<T, TParms> : IDbOpBase
        where T : class
        where TParms : IDtoParm
    {
        Task<TDtoResult> Do<TDtoResult>(
            TParms parms, 
            Expression<Func<T, TDtoResult>> projection,
            IDbCtxWrapper? ctx = null,
            CancellationToken cancellationToken = default) where TDtoResult : IDtoResult;
    }
}