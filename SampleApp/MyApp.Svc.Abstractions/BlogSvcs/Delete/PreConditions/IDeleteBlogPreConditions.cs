using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Delete;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Delete.PreConditions;

public interface IDeleteBlogPreConditions: IPreCondition<DeleteBlogParms>
{
}
