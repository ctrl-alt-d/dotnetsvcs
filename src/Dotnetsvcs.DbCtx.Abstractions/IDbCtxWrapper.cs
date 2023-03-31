using Dotnetsvcs.DbCtx.Abstractions.Transactions;
using System.Linq.Expressions;

namespace Dotnetsvcs.DbCtx.Abstractions;

/// <summary>
/// The goal of this interface is to easaly mock db operations in your code.
/// Try to code your logic without full DbContext, just with wrappers.
/// </summary>
public interface IDbCtxWrapper : IDisposable {

    void Attach<T>(T entity);
    ITxWrapper BeginTransaction();

    Task AddAsync<T>(T entity, CancellationToken cancellationToken = default)
        where T : class;

    void Remove<T>(T entity)
        where T : class;

    IQueryable<T> Set<T>()
        where T : class;

    IQueryable<T> SetAsNoTracking<T>()
        where T : class;

    Task<TProjection> FirstWithProjectionAsync<T, TProjection>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, bool>> where,
        Expression<Func<T, TProjection>> projection
        )
        where T : class
        where TProjection : class;

    Task<TProjection?> FirstOrDefaultWithProjectionAsync<T, TProjection>(
        Expression<Func<T, bool>> filter,
        Expression<Func<T, bool>> where,
        Expression<Func<T, TProjection>> projection
        )
        where T : class
        where TProjection : class;


    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    ValueTask<T?> FindAsync<T>(params object?[]? keyValues)
        where T : class;

    Task LoadAsync<T, P>(T entry, Expression<Func<T, P?>> reference)
        where T : class
        where P : class;

    Task LoadAsync<T, P>(T entry, Expression<Func<T, IEnumerable<P>>> reference)
       where T : class
       where P : class;

    /// <summary>
    /// Be carefull, calling this method your code become less Mockable.
    /// Try to code your logic only with wrapped methods, not with full dbContext.
    /// </summary>
    /// <typeparam name="TFullDbContext"></typeparam>
    /// <returns></returns>
    TFullDbContext GetDbContext<TFullDbContext>() where TFullDbContext : class;
}