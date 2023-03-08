using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using System.Linq.Expressions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs;

public static class BlogDefaultProjection
{
    public static Expression<Func<Blog, BlogDtoResult>> ToDtoResult =>
        blog => new BlogDtoResult
        {
            Titol = blog.Titol,
            Preu = blog.Preu,
            TimeStamp = blog.TimeStamp,
            CategoriaKey = blog.Categoria == null ? null : new object[] { blog.Categoria!.Id },
            CategoriaDisplay = blog.Categoria == null ? "" : blog.Categoria!.Titol,
            Rating = blog.Rating
        };
}
