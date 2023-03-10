using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.MSDbContext;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Retrieve;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.Integration.Test;

public class ServiceCanRetrieveBlogsTest {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ServiceCanRetrieveBlogsTest() {
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
    public async Task RetrieveingBlogsTest() {
        // Arrange
        var blog = new Blog() { Rating = 10, Titol="Blog test title" };
        Ctx.Add(blog);
        Ctx.SaveChanges();

        using var createPostSvc =
            ServiceProvider
            .GetRequiredService<IRetrieveBlogService>();

        // Act
        var qs =
            await
            createPostSvc
            .Do();

        var data =
            qs
            .Count();

        createPostSvc
            .Dispose();

        var log =
            Logger
            .LogLastEntry;

        // Assert
        log
            .Should()
            .Be("SELECT COUNT(*) FROM Blog AS b");

        data
            .Should()
            .Be(1);
    }


    [Fact]
    public async Task UnableToQueryDisposedService() {
        // Arrange
        var blog = new Blog() { Rating = 10, Titol="Blog test title" };
        Ctx.Add(blog);
        Ctx.SaveChanges();

        using var createPostSvc =
            ServiceProvider
            .GetRequiredService<IRetrieveBlogService>();

        // Act
        var qs =
            await
            createPostSvc
            .Do();

        createPostSvc
            .Dispose();

        qs
            .Invoking( d => d.Count())
            .Should()
            .Throw<ObjectDisposedException>();
    }

}