using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions.Exceptions;
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

public class PreConditionSimpleTest {
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public PreConditionSimpleTest() {
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
    public async Task FactoryCanCreateWrappedContext() {
        // Arrange
        using var createBlogSvc =
            ServiceProvider
            .GetRequiredService<ICreateBlogService>();

        using var dbCtxWrapper =
            ServiceProvider
            .GetRequiredService<IDbCtxWrapperFactory>()
            .CreateCtx();

        var parm1 =
            new CreateBlogParms() {
                Rating = 10,
                Titol = "hola",
            };

        var parm2 =
            new CreateBlogParms() {
                Rating = 20,
                Titol = parm1.Titol,
            };

        var projection =
            ServiceProvider
            .GetRequiredService<IBlogDefaultProjection>();

        using var tx =
            dbCtxWrapper.BeginTransaction();

        var blog1 =
            await createBlogSvc.Do(parm1, projection, dbCtxWrapper);

        // Act && Assert
        await createBlogSvc
            .Awaiting(
               c => createBlogSvc.Do(parm2, projection, dbCtxWrapper)
            )
            .Should()
            .ThrowAsync<SvcException>()
            .WithMessage($"Already exists Blog with title: {parm2.Titol}");

        tx.Rollback();

        Ctx
            .Set<Blog>()
            .Should()
            .HaveCount(0);

    }
}