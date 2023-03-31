using MyApp.DtoData.BlogDtosData;
using MyApp.Projections.Abstractions.BlogProjections;

namespace MyApp.Projections.BlogProjections;

public class BlogDefaultProjection : IBlogDefaultProjection
{
    public BlogDefaultProjection(IPostDefaultFilter postFilter) {
        PostFilter=postFilter;
    }

    protected virtual IPostDefaultFilter PostFilter { get; }

    public void Dispose() {
        PostFilter.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<Expression<Func<Blog, BlogDtoData>>> GetToDtoData(IDbCtxWrapper ctx)
    {
        var filter = await PostFilter.GetFilter(ctx);
        return blog => new BlogDtoData
        {
            Id = blog.Id,
            Titol = blog.Title,
            Preu = blog.Price,
            TimeStamp = blog.TimeStamp,
            CategoriaKey = blog.Category == null ? null : new object[] { blog.Category!.Id },
            CategoriaDisplay = blog.Category == null ? "" : blog.Category!.Name,
            Rating = blog.Rating,
            NumPostsCalculated = blog.Posts.AsQueryable().Where(filter).Count(),
        };
    }
}
