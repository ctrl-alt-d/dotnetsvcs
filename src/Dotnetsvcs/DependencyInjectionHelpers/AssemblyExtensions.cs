using System.Reflection;

namespace Dotnetsvcs.DependencyInjectionHelpers;

internal static class AssemblyExtensions {

    internal static List<DIItem> TypesImplementingTarget(this Assembly assemblyImplementations, Assembly assemblyAbstractions, Type target) {
        var types =
            assemblyImplementations
            .GetTypes()
            .ToList();

        var interfaces =
            assemblyAbstractions
            .GetTypes()
            .Where(t => t.IsInterface)
            .ToList();

        var implementations =
            types
            .Where(t => !t.IsAbstract && !t.IsInterface)
            .Where(t =>
                (
                    t.BaseType != null && t.BaseType!.IsGenericType && t.BaseType!.GetGenericTypeDefinition() == target) ||
                    t.GetInterfaces().Where(i => i.IsGenericType).Any(i => i.GetGenericTypeDefinition() == target) ||
                    t.GetInterfaces().Any(i => i == target)
                )
            .ToList();

        var implementationInterfaceList =
            implementations
            .Select(implementationType =>
                (
                    implementationType,
                    interfaceType: implementationType.MyInterface(interfaces)
                )
            )
            .ToList();

        var notFoundInteface =
            implementationInterfaceList
            .Where(i => i.interfaceType == null)
            .Select(i => i.implementationType.Name)
            .ToList();

        if (notFoundInteface.Any()) {
            var msg = "Unable to locate interface for " +
                string.Join(", ", notFoundInteface);
            throw new Exception(msg);
        }

        return
            implementationInterfaceList
            .Select(i => new DIItem(i.interfaceType!, i.implementationType))
            .ToList();
    }

    private static Type? MyInterface(this Type implementationType, List<Type> intefaces) {
        return intefaces.FirstOrDefault(t => t.Name == $"I{implementationType.Name}");
    }
}
