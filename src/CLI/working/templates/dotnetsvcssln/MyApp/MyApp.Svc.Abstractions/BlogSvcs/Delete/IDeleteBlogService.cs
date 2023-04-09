using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Delete;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Delete;

public interface IDeleteBlogService: IDbOpDelete<Blog, DeleteBlogParms>
{
}
