using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Abstractions.BaseOps;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc;

public class SvcFactory<T> : ISvcFactory<T> where T : IDbOpBase {
    public SvcFactory(IServiceProvider serviceProvider) {
        ServiceProvider=serviceProvider;
    }

    private IServiceProvider ServiceProvider { get; }

    public T Create()        
        =>
        ServiceProvider
        .GetRequiredService<T>();


}
