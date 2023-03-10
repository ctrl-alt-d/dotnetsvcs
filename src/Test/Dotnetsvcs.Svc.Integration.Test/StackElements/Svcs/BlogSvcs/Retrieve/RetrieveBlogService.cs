using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Retrieve;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Retrieve;
internal class RetrieveBlogService : DbOpRetrieve<Blog>, IRetrieveBlogService {
    public RetrieveBlogService(IDbCtxWrapperFactory dbCtxWrapperFactory) : base(dbCtxWrapperFactory) {
    }

    protected override async Task<Expression<Func<Blog, bool>>> GetFilterExpression() {

        await Task.CompletedTask;
        return (_) => true;
    }
}
