using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Common.Filters;
using System.Linq.Expressions;

namespace MyApp.Svcs.BlogSvcs.Common.Filters;

public class BlogDefaultFilter : IBlogDefaultFilter
{
    public async Task<Expression<Func<Blog, bool>>> GetFilter(IDbCtxWrapper ctx)
    {
        await Task.CompletedTask;
        return  blog => !blog.IsDeleted;
    }
}
