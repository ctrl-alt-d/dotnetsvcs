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

public class ServiceCanCreateBlogsSimpleTest {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ServiceCanCreateBlogsSimpleTest() {
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
    public async Task CreatingBlogsTest() {
        // Arrange
        using var createBlogSvc =
            ServiceProvider
            .GetRequiredService<ICreateBlogService>();

        var parm1 =
            new CreateBlogParms() {
                Rating = 10,
                Titol = "hola",
            };

        var expected1 = new {
            Rating = 10,
            Titol = "hola",
        };

        var parm2 =
            new CreateBlogParms() {
                Rating = 20,
                Titol = "adeu",
            };

        var expected2 = new {
            Rating = 20,
            Titol = "adeu",
        };

        var projection =
            ServiceProvider
            .GetRequiredService<IBlogDefaultProjection>();

        // Act

        var blog1 =
            await createBlogSvc.Do(parm1, projection);

        var blog2 =
            await createBlogSvc.Do(parm2, projection);

        createBlogSvc.Dispose();

        // Assert
        Ctx
            .Set<Blog>()
            .Should()
            .HaveCount(2);

        Ctx
            .Set<Blog>()
            .Should()
            .ContainEquivalentOf(expected1);

        Ctx
            .Set<Blog>()
            .Should()
            .ContainEquivalentOf(expected2);

        blog1
            .Should()
            .BeEquivalentTo(expected1);

        blog2
            .Should()
            .BeEquivalentTo(expected2);

        blog1
            .TimeStamp
            .Should()
            .NotBeSameAs(blog2.TimeStamp);
    }
}