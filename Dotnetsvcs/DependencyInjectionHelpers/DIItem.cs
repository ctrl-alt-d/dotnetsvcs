namespace Dotnetsvcs.DependencyInjectionHelpers;

internal class DIItem
{
    public DIItem(Type interfaceType, Type implementationType)
    {
        InterfaceType=interfaceType;
        ImplementationType=implementationType;
    }

    public Type InterfaceType { get; }
    public Type ImplementationType { get; }
}
