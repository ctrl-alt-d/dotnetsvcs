using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Retrieve;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.MSDbContext;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.BlogProjections;
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
        var expected = new { Rating = 10, Titol = "Blog test title" };
        Ctx.Add(blog);
        Ctx.SaveChanges();

        using var createPostSvc =
            ServiceProvider
            .GetRequiredService<IRetrieveBlogService>();

        using var projection =
            ServiceProvider
            .GetRequiredService<IBlogDefaultProjection>();

        // Act
        var parms = new RetrieveBlogParms() {
            ItemsPerPage = 10,
            Page = 0,
            TotalCountRequired = true
        };

        var result =
            await
            createPostSvc
            .Do(parms, projection);

        createPostSvc
            .Dispose();

        var log1 =
            Logger
            .LogSecondToLastEntry;

        var log2 =
            Logger
            .LogLastEntry;

        // Assert
        log1
            .Should()
            .Be(
                "SELECT t.Titol, t.Preu, t.TimeStamp, c.Id IS NULL, c.Id, " +
                "CASE WHEN c.Id IS NULL THEN '' ELSE c.Titol END, t.Rating, " +
                "( SELECT COUNT(*) FROM Post AS p WHERE t.Id = p.BlogId AND NOT (p.IsSoftDeleted)) " + //<-- Aggregation
                "FROM ( SELECT b.Id, b.CategoriaId, b.Preu, b.Rating, b.TimeStamp, b.Titol " +
                "FROM Blog AS b LIMIT @__p_1 OFFSET @__p_0 ) AS t " +
                "LEFT JOIN Categoria AS c ON t.CategoriaId = c.Id"
                );

        log2
            .Should()
            .Be("SELECT COUNT(*) FROM Blog AS b");

        result
            .Items
            .Should()
            .ContainEquivalentOf(expected);

        result
            .TotalCount
            .Should()
            .Be(1);
    }


}