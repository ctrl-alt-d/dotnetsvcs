using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Retrieve;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Retrieve;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.Abstractions.BlogFilters;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Retrieve;
public class RetrieveBlogService : DbOpRetrieve<Blog, RetrieveBlogParms>, IRetrieveBlogService {
    public RetrieveBlogService(IDbCtxWrapperFactory dbCtxWrapperFactory, IBlogDefaultFilter filter) : 
        base(dbCtxWrapperFactory, filter) {
    }

    protected override async Task<Expression<Func<Blog, bool>>> GetWhereFromParms(RetrieveBlogParms parms) {
        await Task.CompletedTask;
        return _ => true;
    }
}
