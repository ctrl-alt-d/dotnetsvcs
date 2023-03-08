using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DbCtx.DependencyInjection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.DbCtx.Test;

public class InjectionTests
{
    public class MyDbContext : DbContext
    { }

    [Fact]
    public void WrapperFactoryIsInjectedProperly()
    {
        // Arrange
        var serviceProvider =
            new ServiceCollection()
            .AddDbContextFactory<MyDbContext>()
            .AddDotnetsvcDbCtx<MyDbContext>()
            .BuildServiceProvider();

        // Act
        var factory =
            serviceProvider
            .GetRequiredService<IDbCtxWrapperFactory>();

        // Assert
        factory
            .Should()
            .BeOfType<DbCtxWrapperFactory<MyDbContext>>();

        factory
            .Should()
            .BeAssignableTo<IDbCtxWrapperFactory>();
    }
}