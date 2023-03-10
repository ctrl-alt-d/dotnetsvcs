using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoData.PostDtosData;
using MyApp.Models;
using MyApp.Projections.Abstractions.PostProjections;
using System.Linq.Expressions;

namespace MyApp.Projections.PostProjections;

public class PostDefaultProjection : IPostDefaultProjection
{
    public void Dispose() {        
    }

    public Expression<Func<Post, PostDtoData>> GetToDtoData(IDbCtxWrapper dbCtxWrapper)
    {

        var totalNumberOfBlogs = dbCtxWrapper.Set<Blog>().Count();
        var NumerTwo = 2;

        return Post => new PostDtoData
        {
            Descripcio = Post.Descripcio,
            EsVisible = Post.EsVisible,
            BlogDisplay = Post.Blog.Titol,
            BlogKey = new object[] { Post.Blog!.Id },
            StatisticsTotalBlogs = totalNumberOfBlogs,
            NumberTwoFromRandomService = NumerTwo,
        };
    }
}
