using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IProjectorFactory<T> where T : IsProjection {
    T Create();
}