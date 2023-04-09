using Dotnetsvcs.Svc;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Retrieve;
using System.Linq.Expressions;
using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.Svcs.Abstractions.BlogSvcs.Common.Filters;
using MyApp.DtoParm.BlogParm.Retrieve;
using Dotnetsvcs.Svc.Criterias;

namespace MyApp.Svcs.BlogSvcs.Retrieve;
public class RetrieveBlogService : DbOpRetrieve<Blog, RetrieveBlogParms>, IRetrieveBlogService {
    public RetrieveBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory, 
        IBlogDefaultFilter filter) : base(dbCtxWrapperFactory, filter) {
    }

    protected override async Task<Expression<Func<Blog, bool>>> GetWhereFromParms(RetrieveBlogParms parms)
    {
        await Task.CompletedTask;

        var titolWhere = parms.TitolCriteria.WhereExpression((Blog b) => b.Title);
        var ratingWhere = parms.RatingCriteria.WhereExpression((Blog b) => b.Rating);

        return AndCriterias.And(titolWhere, ratingWhere);
    }
}
