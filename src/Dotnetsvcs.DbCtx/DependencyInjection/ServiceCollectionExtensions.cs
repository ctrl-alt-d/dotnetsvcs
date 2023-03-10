using Dotnetsvcs.DbCtx.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetsvcs.DbCtx.DependencyInjection;

public static class ServiceCollectionExtensions {
    public static IServiceCollection AddDotnetsvcDbCtx<T>(this IServiceCollection serviceCollection)
        where T : DbContext
        =>
        serviceCollection
        .AddSingleton<IDbCtxWrapperFactory, DbCtxWrapperFactory<T>>();
}
