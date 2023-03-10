using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.PostParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.MSDbContext;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.PostProjections;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.PostSvcs.Create;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.Integration.Test;

public class ProjectionCanAccessOtherServices {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ProjectionCanAccessOtherServices() {
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
    public async Task GettingDataFromCtxAndOtherServices() {
        // Arrange
        var blog = new Blog() { Rating =10, Titol="Blog test title" };
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
                NumberTwoFromRandomService = 2,
                StatisticsTotalBlogs = 1
            };

        var projection =
            ServiceProvider
            .GetRequiredService<IPostDefaultProjection>();

        // Act
        var postDto =
            await createPostSvc.Do(parm, projection);

        // Assert
        postDto
            .Should()
            .BeEquivalentTo(expectedDto);
    }
}