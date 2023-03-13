using Dotnetsvcs.DbCtx.Abstractions;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Abstractions;
public interface IFilter<T>
{
    public Expression<Func<T, bool>> GetFilter(IDbCtxWrapper ctx);
}
