using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.Models;
using MyApp.Svcs.Abstractions.PostSvcs.Common.Filters;
using System.Linq.Expressions;

namespace MyApp.Svcs.PostSvcs.Common.Filters;

public class PostDefaultFilter : IPostDefaultFilter
{
    public Expression<Func<Post, bool>> GetFilter(IDbCtxWrapper ctx)
    {
        return _ => true;
    }
}
