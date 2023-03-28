using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.MSDbContext;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.BlogProjections;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.Integration.Test;

public class ServiceSqlTest {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ServiceSqlTest() {
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
    public async Task ExpectedSqlGenerationTest() {
        // Arrange
        using var createBlogSvc =
            ServiceProvider
            .GetRequiredService<ICreateBlogService>();

        var parm =
            new CreateBlogParms() {
                Rating = 10,
                Titol = "hola",
            };

        var projection =
            ServiceProvider
            .GetRequiredService<IBlogDefaultProjection>();

        var expectedPrecondition =
            "SELECT EXISTS ( SELECT 1 FROM Blog AS b WHERE b.Titol = @__parms_Titol_0)";

        var expectedInsert =
            "INSERT INTO Blog (CategoriaId, EsVisible, Preu, Rating, Titol) " +
            "VALUES (@p0, @p1, @p2, @p3, @p4) RETURNING Id, TimeStamp;";

        var expectedProjection =
            "SELECT b.Titol, b.Preu, b.TimeStamp, c.Id IS NULL, c.Id, " +
            "CASE WHEN c.Id IS NULL THEN '' ELSE c.Titol END, b.Rating, " +
            "( SELECT COUNT(*) FROM Post AS p WHERE b.Id = p.BlogId AND NOT (p.IsSoftDeleted)) " + // <-- aggregation with filter
            "FROM Blog AS b LEFT JOIN Categoria AS c ON b.CategoriaId = c.Id " +
            "WHERE b.Id = @__entity_equality_entity_0_Id LIMIT 1";

        // Act
        var blogDto =
            await createBlogSvc.Do(parm, projection);

        var logs = Logger.Log;

        // Assert
        logs
            .Should()
            .Contain(expectedPrecondition);

        logs
            .Should()
            .Contain(expectedInsert);

        logs
            .Should()
            .Contain(expectedProjection);


    }
}