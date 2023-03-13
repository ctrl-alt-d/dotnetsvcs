using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Abstractions.HelperInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc;

public class ProjectionFactory<T> : IProjectionFactory<T> where T : IsProjection {
    public ProjectionFactory(IServiceProvider serviceProvider) {
        ServiceProvider=serviceProvider;
    }

    private IServiceProvider ServiceProvider { get; }

    public T Create()
        
        =>
        ServiceProvider
        .GetRequiredService<T>();


}
