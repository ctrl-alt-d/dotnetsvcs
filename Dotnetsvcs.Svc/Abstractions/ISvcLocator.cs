using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.Abstractions
{
    public interface ISvcLocator
    {
        T LocateSvc<T>() where T : IDbOpBase;
    }
}