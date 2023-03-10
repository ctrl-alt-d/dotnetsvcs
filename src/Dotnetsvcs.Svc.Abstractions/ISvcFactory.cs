using Dotnetsvcs.Svc.Abstractions.BaseOps;

namespace Dotnetsvcs.Svc.Abstractions; 
public interface ISvcFactory<T> where T : IDbOpBase {
    T Create();
}