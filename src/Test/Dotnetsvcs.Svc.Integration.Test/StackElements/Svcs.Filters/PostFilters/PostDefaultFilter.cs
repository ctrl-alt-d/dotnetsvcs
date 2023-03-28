using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.Abstractions.PostFilters;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.PostFilters;

public class PostDefaultFilter : IPostDefaultFilter {
    public async Task<Expression<Func<Post, bool>>> GetFilter(IDbCtxWrapper ctx) {
        await Task.CompletedTask;
        return post => !post.IsSoftDeleted;
    }
}
