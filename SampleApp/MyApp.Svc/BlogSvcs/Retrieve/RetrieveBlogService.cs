using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc;
using MyApp.Models;
using System.Linq.Expressions;
using MyApp.Svcs.Abstractions.BlogSvcs.Retrieve;

namespace MyApp.Svcs.BlogSvcs.Retrieve;
internal class RetrieveBlogService : DbOpRetrieve<Blog>, IRetrieveBlogService
{
    public RetrieveBlogService(IDbCtxWrapperFactory dbCtxWrapperFactory) : base(dbCtxWrapperFactory)
    {
    }

    protected override async Task<Expression<Func<Blog, bool>>> GetFilterExpression()
    {

        await Task.CompletedTask;
        return (_) => true;
    }
}
