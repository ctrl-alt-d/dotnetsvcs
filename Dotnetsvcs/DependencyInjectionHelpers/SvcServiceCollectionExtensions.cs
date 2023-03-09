using Dotnetsvcs.Svc;
using Dotnetsvcs.Svc.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dotnetsvcs.DependencyInjectionHelpers;

internal static class SvcServiceCollectionExtensions
{
    internal static IServiceCollection AddPreconditions(this IServiceCollection serviceCollection, Assembly assembly)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assembly, typeof(IPreCondition<>));

    internal static IServiceCollection AddPostConditions(this IServiceCollection serviceCollection, Assembly assembly)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assembly, typeof(IPostCondition<,>));

    internal static IServiceCollection AddCreate(this IServiceCollection serviceCollection, Assembly assembly)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assembly, typeof(DbOpCreate<,>));

    internal static IServiceCollection AddUpdate(this IServiceCollection serviceCollection, Assembly assembly)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assembly, typeof(DbOpUpdate<,>));

    internal static IServiceCollection AddDelete(this IServiceCollection serviceCollection, Assembly assembly)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assembly, typeof(DbOpDelete<,>));

    internal static IServiceCollection AddRetrieve(this IServiceCollection serviceCollection, Assembly assembly)
        =>
        serviceCollection
        .AddWithInterfaceIfImplementsGenericInterface(assembly, typeof(DbOpRetrieve<,,>));

    private static IServiceCollection AddWithInterfaceIfImplementsGenericInterface(this IServiceCollection serviceCollection, Assembly assembly, Type target)
    {
        var items =
            assembly
            .TypesWithGenericTypeDefinition(target);

        items
            .ForEach(item =>
                serviceCollection
                .AddScoped(item.InterfaceType, item.ImplementationType)
            );

        return serviceCollection;
    }

}
