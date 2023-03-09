using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Abstractions.BaseOps;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc;

public class SvcLocator : ISvcLocator {
    public SvcLocator(IServiceProvider serviceProvider) {
        ServiceProvider=serviceProvider;
    }

    private IServiceProvider ServiceProvider { get; }

    public T LocateSvc<T>()
        where T : IDbOpBase
        =>
        ServiceProvider
        .GetRequiredService<T>();


}
