using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc;

public class ProjectorLocator : IProjectorLocator {
    public ProjectorLocator(IServiceProvider serviceProvider) {
        ServiceProvider=serviceProvider;
    }

    private IServiceProvider ServiceProvider { get; }

    public T Locate<T>()
        where T : IsProjection
        =>
        ServiceProvider
        .GetRequiredService<T>();


}
