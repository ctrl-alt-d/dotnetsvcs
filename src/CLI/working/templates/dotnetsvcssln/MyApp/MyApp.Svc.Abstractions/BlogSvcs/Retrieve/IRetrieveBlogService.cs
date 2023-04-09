using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Retrieve;
using MyApp.Models;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Retrieve;
public interface IRetrieveBlogService : IDbOpRetrieve<Blog, RetrieveBlogParms>
{
}