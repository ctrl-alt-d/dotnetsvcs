using Dotnetsvcs.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.MSDbContext;
using Dotnetsvcs.Svc.Integration.Test.StackElements.OtherRandomServices;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;

public static class ServiceCollectionExtension {
    public static IServiceCollection AddServices(
        this IServiceCollection serviceCollection,
        Action<string>? customLogger = null)
        =>
        //
        serviceCollection

        // Context
        .AddDbContextFactory<TestDbContext>(o => o.ConfigureDbOptions(customLogger))

        // Dotnetsvc
        .AddDotnetsvc<TestDbContext>(assemblySvcs: Assembly.GetAssembly(typeof(ServiceCollectionExtension))!)

        // Other random services
        .AddScoped<IRandomService1, RandomService1>()

        // that's all
        ;

}
