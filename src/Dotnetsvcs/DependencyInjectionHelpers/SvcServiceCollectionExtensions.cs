using Dotnetsvcs.Facade.Abstractions;
using Dotnetsvcs.Svc;
using Dotnetsvcs.Svc.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dotnetsvcs.DependencyInjectionHelpers;

internal static class SvcServiceCollectionExtensions {

    internal static IServiceCollection AddFacade(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(IFacade));


    internal static IServiceCollection AddProjections(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(IProjection<,>));

    internal static IServiceCollection AddPreconditions(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(IPreCondition<>));

    internal static IServiceCollection AddPostConditions(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(IPostCondition<,>));

    internal static IServiceCollection AddCreate(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(DbOpCreate<,>));

    internal static IServiceCollection AddUpdate(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(DbOpUpdate<,>));

    internal static IServiceCollection AddDelete(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(DbOpDelete<,>));

    internal static IServiceCollection AddRetrieve(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assemblyImplementations, assemblyAbstractions, typeof(DbOpRetrieve<>));

    private static IServiceCollection AddWithInterfaceIfImplementsGenericInterface(this IServiceCollection serviceCollection, Assembly assemblyImplementations, Assembly assemblyAbstractions, Type target) {
        var items =
            assemblyImplementations
            .TypesImplementingTarget(assemblyAbstractions, target);

        items
            .ForEach(item =>
                serviceCollection
                .AddTransient(item.InterfaceType, item.ImplementationType)
            );

        return serviceCollection;
    }

}
