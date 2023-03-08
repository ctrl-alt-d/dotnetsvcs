using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DbCtx.Abstractions.Transactions;
using Dotnetsvcs.DbCtx.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Dotnetsvcs.DbCtx;


public class DbCtxWrapper<TDbContext> : IDbCtxWrapper
    where TDbContext : DbContext
{
    protected virtual TDbContext DbContext { get; }

    public DbCtxWrapper(TDbContext dbContext)
    {
        DbContext=dbContext;
    }

    // Tx
    public ITxWrapper BeginTransaction()
        =>
        new TxWrapper(
            DbContext
            .Database
            .BeginTransaction()
        );

    // Create
    public async Task AddAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class
        =>
        await
        DbContext
        .AddAsync(entity, cancellationToken);

    // Remove
    public void Remove<T>(T entity) where T : class
        =>
        DbContext
        .Remove(entity);

    // Retrieve - Collection
    public IQueryable<T> Set<T>()
        where T : class
        =>
        DbContext
        .Set<T>();

    // Retrieve - Entity
    public ValueTask<T?> FindAsync<T>(params object?[]? keyValues) where T : class
        =>
        DbContext
        .FindAsync<T>(keyValues);

    // Save changes
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        =>
        DbContext
        .SaveChangesAsync(cancellationToken);

    // Load navigation properties
    public Task LoadAsync<T, P>(T entry, Expression<Func<T, P?>> reference)
        where T : class
        where P : class
        =>
        DbContext
        .Entry(entry)
        .Reference(reference)
        .LoadAsync();

    public Task LoadAsync<T, P>(T entry, Expression<Func<T, IEnumerable<P>>> reference)
        where T : class
        where P : class
        =>
        DbContext
        .Entry(entry)
        .Collection(reference)
        .LoadAsync();

    public TFullDbContext GetDbContext<TFullDbContext>()
        where TFullDbContext : class
    {
        // Check same type:
        if (DbContext is not TFullDbContext fullCtx)
        {
            var msg =
                $"Wrong context type. " +
                $"My dbContext [{typeof(TDbContext).Name}] does not " +
                $"is [{typeof(TFullDbContext).Name}] ";
            throw new ArgumentException(msg);
        }

        // Return
        return fullCtx;
    }

    public void Dispose()
        =>
        DbContext
        .Dispose();


}
