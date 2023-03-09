using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;

namespace Dotnetsvcs.Svc.Abstractions;

public interface IProjectorLocator {
    T Locate<T>() where T : IsProjection;
}