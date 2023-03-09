using Dotnetsvcs.Svc.Exceptions;

namespace Dotnetsvcs.Svc.CtxWrapperHelpers;

public static class CtxWrapperFindExtensions {
    public static async Task<T> FindOrException<T>(
        this IDbCtxWrapper ctx,
        params object?[]? pk
        )
        where T : class {
        var entity = await ctx.FindAsync<T>(pk);

        if (entity == null) throw new SvcException("Not found");

        return entity;
    }
}
