using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoData.BlogDtosData;
using MyApp.Models;
using MyApp.Projections.Abstractions.BlogProjections;
using System.Linq.Expressions;

namespace MyApp.Projections.BlogProjections;

public class BlogDefaultProjection : IBlogDefaultProjection
{
    public void Dispose() {
    }

    public Expression<Func<Blog, BlogDtoData>> GetToDtoData(IDbCtxWrapper _)
    {
        return blog => new BlogDtoData
        {
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
