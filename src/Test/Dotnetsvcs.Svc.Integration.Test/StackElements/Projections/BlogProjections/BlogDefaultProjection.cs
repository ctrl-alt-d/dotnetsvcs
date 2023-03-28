using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoResult.BlogDtosResult;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.BlogProjections;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.Abstractions.PostFilters;
using System.Linq;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.BlogProjections;

public class BlogDefaultProjection : IBlogDefaultProjection {
    public BlogDefaultProjection(IPostDefaultFilter postFilter) {
        PostFilter=postFilter;
    }

    protected virtual IPostDefaultFilter PostFilter { get;  }

    public void Dispose() {
    }

    public async Task<Expression<Func<Blog, BlogDtoResult>>> GetToDtoData(IDbCtxWrapper ctx) {
        var postFilter = await PostFilter.GetFilter(ctx);
        return blog => new BlogDtoResult {
            Titol = blog.Titol,
            Preu = blog.Preu,
            TimeStamp = blog.TimeStamp,
            CategoriaKey = blog.Categoria == null ? null : new object[] { blog.Categoria!.Id },
            CategoriaDisplay = blog.Categoria == null ? "" : blog.Categoria!.Titol,
            Rating = blog.Rating,
            NumPostsCalculated = blog.Posts.AsQueryable().Where(postFilter).Count(),
        };
    }
}
