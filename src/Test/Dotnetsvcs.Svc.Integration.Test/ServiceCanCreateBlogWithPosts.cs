using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.MSDbContext;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.BlogProjections;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.Integration.Test;

public class ServiceCanCreateBlogWithPosts {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ServiceCanCreateBlogWithPosts() {
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
    public async Task CreatingBlogWithPostTest() {
        // Arrange
        using var createBlogSvc =
            ServiceProvider
            .GetRequiredService<ICreateBlogService>();

        var parm =
            new CreateBlogParms() {
                Rating = 10,
                Titol = "hola",
                WithNposts = 69
            };

        var expectedDto = new {
            Rating = 10,
            Titol = "hola",
            NumPostsCalculated = 69,
        };

        var projection =
            ServiceProvider
            .GetRequiredService<IBlogDefaultProjection>();

        // Act
        var blogDto =
            await createBlogSvc.Do(parm, projection);

        // Assert
        Ctx
            .Set<Blog>()
            .Should()
            .HaveCount(1);

        Ctx
            .Set<Post>()
            .Should()
            .HaveCount(69);

        blogDto
            .Should()
            .BeEquivalentTo(expectedDto);

    }
}