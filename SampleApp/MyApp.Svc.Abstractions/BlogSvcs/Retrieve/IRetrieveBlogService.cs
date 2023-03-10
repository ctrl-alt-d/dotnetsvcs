using Dotnetsvcs.Svc.Abstractions;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Retrieve;
public interface IRetrieveBlogService : IDbOpRetrieve<Blog>
{
}