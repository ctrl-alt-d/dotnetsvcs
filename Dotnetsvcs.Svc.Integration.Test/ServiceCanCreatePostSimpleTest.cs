using Dotnetsvcs.Svc.Integration.Test.StackElements;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.Integration.Test;

public class ServiceCanCreatePostSimpleTest {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ServiceCanCreatePostSimpleTest()
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
    public async Task CreatingPostTest()
    {
        // Arrange
        var blog = new Blog() {Rating =10, Titol="Blog test title" };
        Ctx.Add(blog);
        Ctx.SaveChanges();

        using var createPostSvc =
            ServiceProvider
            .GetRequiredService<ICreatePostService>();

        var parm =
            new CreatePostParms() {
                Descripcio = "Post description test",
                BlogKey = new object?[] { blog.Id }
            };

        var expectedDto =
            new {
                Descripcio = "Post description test",
                BlogDisplay = blog.Titol
            };

        var expectedEntity =
            new {
                Descripcio = "Post description test",
                Blog = blog
            };

        // Act
        var postDto =
            await createPostSvc.Do(parm, PostDefaultProjection.ToDtoResult);

        // Assert
        Ctx
            .Set<Post>()
            .Should()
            .HaveCount(1);

        Ctx
            .Set<Post>()
            .Should()
            .ContainEquivalentOf(expectedEntity);

        postDto
            .Should()
            .BeEquivalentTo(expectedDto);
    }
}