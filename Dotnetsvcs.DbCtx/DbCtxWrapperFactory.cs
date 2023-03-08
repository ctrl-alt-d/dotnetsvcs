using Dotnetsvcs.DbCtx.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Dotnetsvcs.DbCtx;

public class DbCtxWrapperFactory<T> : IDbCtxWrapperFactory
    where T : DbContext
{
    private readonly IDbContextFactory<T> DbContextFactory;

    public DbCtxWrapperFactory(IDbContextFactory<T> dbContextFactory)
    {
        DbContextFactory=dbContextFactory;
    }

    public IDbCtxWrapper CreateCtx()
    {
        var dbCtx =
            DbContextFactory
            .CreateDbContext();

        var dbCtxWrapper =
            new DbCtxWrapper<T>(dbCtx);

        return dbCtxWrapper;
    }
}