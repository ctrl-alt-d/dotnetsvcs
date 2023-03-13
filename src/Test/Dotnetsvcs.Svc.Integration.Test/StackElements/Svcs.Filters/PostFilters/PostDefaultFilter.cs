using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.Abstractions.PostFilters;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.PostFilters;

public class PostDefaultFilter : IPostDefaultFilter {
    public Expression<Func<Post, bool>> GetFilter(IDbCtxWrapper ctx) {
        return post => !post.IsSoftDeleted;
    }
}
