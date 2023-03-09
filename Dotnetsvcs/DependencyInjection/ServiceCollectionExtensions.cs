using Dotnetsvcs.DbCtx.DependencyInjection;
using Dotnetsvcs.DependencyInjectionHelpers;
using Dotnetsvcs.Svc.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dotnetsvcs.DependencyInjection;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDotnetsvc<T>(this IServiceCollection serviceCollection, Assembly assemblySvcs)
        where T : DbContext
        =>
        serviceCollection

        // wrapper dbcontext layer
        .AddDotnetsvcDbCtx<T>()

        // services layer
        .AddDotnetsvcSvc()

        // app services implementations (in app assembly)
        .AddProjections(assemblySvcs)
        .AddPreconditions(assemblySvcs)
        .AddPostConditions(assemblySvcs)
        .AddCreate(assemblySvcs)
        .AddUpdate(assemblySvcs)
        .AddDelete(assemblySvcs)
        .AddRetrieve(assemblySvcs)

        // thats all, thanks for watching
        ;
}
