using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoResult.BlogDtosResult;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.BlogProjections;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.BlogProjections;

public class BlogDefaultProjection : IBlogDefaultProjection {
    public void Dispose() {
    }

    public Expression<Func<Blog, BlogDtoResult>> GetToDtoData(IDbCtxWrapper _) {
        return blog => new BlogDtoResult {
            Titol = blog.Titol,
            Preu = blog.Preu,
            TimeStamp = blog.TimeStamp,
            CategoriaKey = blog.Categoria == null ? null : new object[] { blog.Categoria!.Id },
            CategoriaDisplay = blog.Categoria == null ? "" : blog.Categoria!.Titol,
            Rating = blog.Rating,
            NumPostsCalculated = blog.Posts.Count(),
        };
    }
}
