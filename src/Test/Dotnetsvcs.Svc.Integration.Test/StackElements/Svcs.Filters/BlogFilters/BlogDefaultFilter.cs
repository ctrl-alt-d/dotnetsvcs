using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.Abstractions.BlogFilters;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.BlogFilters;

public class BlogDefaultFilter : IBlogDefaultFilter {
    public Expression<Func<Blog, bool>> GetFilter(IDbCtxWrapper ctx) {
        return _ => true;
    }
}
