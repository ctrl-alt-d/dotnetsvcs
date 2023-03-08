using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DbCtx.Transactions;
using Dotnetsvcs.Svc.Integration.Test.StackElements;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DependencyInjection;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.TestUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.Svc.Integration.Test;

public class ServiceCanCreateBlogsWithTxSimpleTest
{
    private readonly FakeLogger Logger;
    private readonly ServiceProvider ServiceProvider;
    private readonly TestDbContext Ctx;

    public ServiceCanCreateBlogsWithTxSimpleTest()
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
    public async Task FactoryCanCreateWrappedContext()
    {
        // Arrange
        using var createBlogSvc =
            ServiceProvider
            .GetRequiredService<ICreateBlogService>();

        using var dbCtxWrapper =
            ServiceProvider
            .GetRequiredService<IDbCtxWrapperFactory>()
            .CreateCtx();

        var parm1 =
            new CreateBlogParms()
            {
                Rating = 10,
                Titol = "hola",
            };

        var parm2 =
            new CreateBlogParms()
            {
                Rating = 20,
                Titol = "adeu",
            };

        // Act
        using var tx = 
            dbCtxWrapper.BeginTransaction();

        var blog1 =
            await createBlogSvc.Do(parm1, BlogDefaultProjection.ToDtoResult, dbCtxWrapper);

        var blog2 =
            await createBlogSvc.Do(parm2, BlogDefaultProjection.ToDtoResult, dbCtxWrapper);

        tx.Commit();
        tx.Dispose();
        dbCtxWrapper.Dispose();
        createBlogSvc.Dispose();

        // Assert
        Ctx
            .Set<Blog>()
            .Should()
            .HaveCount(2);

        Ctx
            .Set<Blog>()
            .Should()
            .ContainEquivalentOf(parm1);

        Ctx
            .Set<Blog>()
            .Should()
            .ContainEquivalentOf(parm2);

        blog1
            .Should()
            .BeEquivalentTo(parm1);

        blog2
            .Should()
            .BeEquivalentTo(parm2);

        blog1
            .TimeStamp
            .Should()
            .NotBeSameAs(blog2.TimeStamp);
    }
}