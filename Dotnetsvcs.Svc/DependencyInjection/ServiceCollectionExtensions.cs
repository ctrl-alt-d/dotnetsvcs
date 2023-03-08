using Dotnetsvcs.Svc.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDotnetsvcSvc(this IServiceCollection serviceCollection)
        =>
        serviceCollection
        .AddScoped<ISvcLocator, SvcLocator>();
}
