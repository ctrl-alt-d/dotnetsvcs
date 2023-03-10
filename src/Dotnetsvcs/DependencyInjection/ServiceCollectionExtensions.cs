using Dotnetsvcs.DbCtx.DependencyInjection;
using Dotnetsvcs.DependencyInjectionHelpers;
using Dotnetsvcs.Svc.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dotnetsvcs.DependencyInjection;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDotnetsvc<T>(
        this IServiceCollection serviceCollection,
        Assembly assembySvcImplementations,
        Assembly assemblySvcAbstractions,
        Assembly assembyProjectionsImplementations,
        Assembly assemblyProjectionsAbstractions,
        Assembly assembyFacadeImplementations,
        Assembly assemblyFacadeAbstractions
        )
        where T : DbContext
        =>
        serviceCollection

        // fw wrapper dbcontext layer
        .AddDotnetsvcDbCtx<T>()

        // fw services (locators)
        .AddDotnetsvcSvc()

        // app facade
        .AddFacade(assembyFacadeImplementations, assemblyFacadeAbstractions)

        // app projections
        .AddProjections(assembyProjectionsImplementations, assemblyProjectionsAbstractions)

        // app services
        .AddPreconditions(assembySvcImplementations, assemblySvcAbstractions)
        .AddPostConditions(assembySvcImplementations, assemblySvcAbstractions)
        .AddCreate(assembySvcImplementations, assemblySvcAbstractions)
        .AddUpdate(assembySvcImplementations, assemblySvcAbstractions)
        .AddDelete(assembySvcImplementations, assemblySvcAbstractions)
        .AddRetrieve(assembySvcImplementations, assemblySvcAbstractions)

        // thats all, thanks for watching
        ;
}
