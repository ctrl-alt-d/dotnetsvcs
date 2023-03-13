using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.Models;
using MyApp.Svcs.Abstractions.PostSvcs;
using System.Linq.Expressions;

namespace MyApp.Svcs.PostSvcs;

public class PostDefaultFilter : IPostDefaultFilter
{
    public Expression<Func<Post, bool>> GetFilter(IDbCtxWrapper ctx)
    {
        return _ => true;
    }
}
