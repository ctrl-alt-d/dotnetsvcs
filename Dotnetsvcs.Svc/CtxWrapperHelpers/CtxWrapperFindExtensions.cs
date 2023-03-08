using Dotnetsvcs.Svc.Exceptions;

namespace Dotnetsvcs.Svc.CtxWrapperHelpers;

public static class CtxWrapperFindExtensions
{
    public static async Task<T> FindOrException<T>(
        this IDbCtxWrapper ctx,
        object?[]? pk,
        string? msg = null,
        CancellationToken cancellationToken = default
        )
        where T : class
    {
        var entity = await ctx.FindAsync<T>(pk, cancellationToken);

        if (entity == null) throw new SvcException(msg ?? "No s'ha trobat aquesta dada");

        return entity;
    }
}
