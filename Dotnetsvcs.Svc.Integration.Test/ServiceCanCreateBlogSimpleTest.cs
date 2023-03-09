using Dotnetsvcs.Svc.Integration.Test.StackElements;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Artifacts;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.Integration.Test;

public class ServiceCanCreateBlogSimpleTest {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ServiceCanCreateBlogSimpleTest()
    {
        // Arrange (Environment)
        Logger =
            new FakeLogger();

        ServiceProvider =
            new ServiceCollection()
            .AddServices(Logger.SaveLog)
            .BuildServiceProvider();

        Ctx =
            ServiceProvider
            .GetRequiredService<IDbContextFactory<TestDbContext>>()
            .CreateDbContext();

        Ctx
            .Database
            .EnsureCreated();
    }

    [Fact]
    public async Task CreatingBlogTest()
    {
        // Arrange
        using var createBlogSvc =
            ServiceProvider
            .GetRequiredService<ICreateBlogService>();

        var parm =
            new CreateBlogParms()
            {
                Rating = 10,
                Titol = "hola",
            };

        var expectedEntity = new {
            Rating = 10,
            Titol = "hola",
        };

        var expectedDto = new {
            Rating = 10,
            Titol = "hola",
            NumPostsCalculated = 0,
        };

        // Act
        var blogDto =
            await createBlogSvc.Do(parm, BlogDefaultProjection.ToDtoResult);

        // Assert
        Ctx
            .Set<Blog>()
            .Should()
            .HaveCount(1);

        Ctx
            .Set<Blog>()
            .Should()
            .ContainEquivalentOf(expectedEntity);

        blogDto
            .Should()
            .BeEquivalentTo(expectedDto);

        blogDto
            .TimeStamp
            .Should()
            .NotBeSameAs(default);
    }
}