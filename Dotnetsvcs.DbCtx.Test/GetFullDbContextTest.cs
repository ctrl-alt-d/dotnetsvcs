using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DbCtx.DependencyInjection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.DbCtx.Test;

public class GetFullDbContextTest
{
    public class MyDbContext : DbContext
    { }

    [Fact]
    public void FullContextGetter()
    {
        // Arrange
        using var ctx =
            new ServiceCollection()
            .AddDbContextFactory<MyDbContext>()
            .AddDotnetsvcDbCtx<MyDbContext>()
            .BuildServiceProvider()
            .GetRequiredService<IDbCtxWrapperFactory>()
            .CreateCtx();

        // Act
        var fullctx =
            ctx.GetDbContext<MyDbContext>();

        // Assert
        fullctx
            .Should()
            .BeOfType<MyDbContext>();
    }

    [Fact]
    public void FullContextGetter_ThrowsExecptionIfBadContextRequested()
    {
        // Arrange
        using var ctx =
            new ServiceCollection()
            .AddDbContextFactory<MyDbContext>()
            .AddDotnetsvcDbCtx<MyDbContext>()
            .BuildServiceProvider()
            .GetRequiredService<IDbCtxWrapperFactory>()
            .CreateCtx();

        // Assert & Act
        ctx
            .Invoking(c => c.GetDbContext<string>())
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Wrong context type.*");
    }
}