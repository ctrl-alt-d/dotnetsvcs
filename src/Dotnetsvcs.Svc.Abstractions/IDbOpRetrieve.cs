using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.Abstractions;
public interface IDbOpRetrieve<T>: IDbOpBase
    where T: class
{
    Task<IQueryable<T>> Do();
}
