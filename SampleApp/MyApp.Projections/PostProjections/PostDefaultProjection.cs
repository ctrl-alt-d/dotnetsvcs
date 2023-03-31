using MyApp.DtoData.PostDtosData;
using MyApp.Projections.Abstractions.PostProjections;

namespace MyApp.Projections.PostProjections;

public class PostDefaultProjection : IPostDefaultProjection
{
    public void Dispose() {     
        GC.SuppressFinalize(this);
    }

    public async Task<Expression<Func<Post, PostDtoData>>> GetToDtoData(IDbCtxWrapper dbCtxWrapper)
    {

        await Task.CompletedTask;
        var totalNumberOfBlogs = dbCtxWrapper.Set<Blog>().Count();
        var NumerTwo = 2;

        return Post => new PostDtoData
        {
            Descripcio = Post.Descripcio,
            EsVisible = Post.EsVisible,
            BlogDisplay = Post.Blog.Title,
            BlogKey = new object[] { Post.Blog!.Id },
            StatisticsTotalBlogs = totalNumberOfBlogs,
            NumberTwoFromRandomService = NumerTwo,
        };
    }
}
