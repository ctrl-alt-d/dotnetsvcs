using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoData.BlogDtosData;
using MyApp.Models;
using MyApp.Projections.Abstractions.BlogProjections;
using MyApp.Svcs.Abstractions.PostSvcs.Common.Filters;
using System.Linq.Expressions;

namespace MyApp.Projections.BlogProjections;

public class BlogDefaultProjection : IBlogDefaultProjection
{
    public BlogDefaultProjection(IPostDefaultFilter postFilter) {
        PostFilter=postFilter;
    }

    protected virtual IPostDefaultFilter PostFilter { get; }

    public void Dispose() {
    }

    public Expression<Func<Blog, BlogDtoData>> GetToDtoData(IDbCtxWrapper ctx)
    {
        var filter = PostFilter.GetFilter(ctx);
        return blog => new BlogDtoData
        {
            Titol = blog.Titol,
            Preu = blog.Preu,
            TimeStamp = blog.TimeStamp,
            CategoriaKey = blog.Categoria == null ? null : new object[] { blog.Categoria!.Id },
            CategoriaDisplay = blog.Categoria == null ? "" : blog.Categoria!.Titol,
            Rating = blog.Rating,
            NumPostsCalculated = blog.Posts.AsQueryable().Where(filter).Count(),
        };
    }


}
