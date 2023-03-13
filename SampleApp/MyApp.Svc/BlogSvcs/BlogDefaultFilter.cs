using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs;
using System.Linq.Expressions;

namespace MyApp.Svcs.BlogSvcs;

public class BlogDefaultFilter : IBlogDefaultFilter
{
    public Expression<Func<Blog, bool>> GetFilter(IDbCtxWrapper ctx)
    {
        return _ => true;
    }
}
