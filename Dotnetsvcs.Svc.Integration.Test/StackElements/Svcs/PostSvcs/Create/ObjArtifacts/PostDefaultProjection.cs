using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

public static class PostDefaultProjection
{
    public static Expression<Func<Post, PostDtoResult>> ToDtoResult =>
        Post => new PostDtoResult
        {
            Descripcio = Post.Descripcio,
            EsVisible = Post.EsVisible,
            BlogDisplay = Post.Blog.Titol,
            BlogKey = new object[] { Post.Blog!.Id },
        };
}
