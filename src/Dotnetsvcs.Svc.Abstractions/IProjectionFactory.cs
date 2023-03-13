using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IProjectionFactory<T> where T : IsProjection {
    T Create();
}