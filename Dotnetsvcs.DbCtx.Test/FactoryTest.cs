using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DbCtx.DependencyInjection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.DbCtx.Test;

public class FactoryTest {
    public class MyDbContext : DbContext { }

    [Fact]
    public void FactoryCanCreateWrappedContext() {
        // Arrange
        var factory =
            new ServiceCollection()
            .AddDbContextFactory<MyDbContext>()
            .AddDotnetsvcDbCtx<MyDbContext>()
            .BuildServiceProvider()
            .GetRequiredService<IDbCtxWrapperFactory>();

        // Act
        using var ctx =
            factory
            .CreateCtx();

        // Assert
        ctx
            .Should()
            .BeOfType<DbCtxWrapper<MyDbContext>>();

        ctx
            .Should()
            .BeAssignableTo<IDbCtxWrapper>();

    }
}