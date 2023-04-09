using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Create;

namespace MyApp.Svcs.Abstractions.BlogSvcs.Create.PreConditions
{
    public interface IDuplicateTitle : IPreCondition<CreateBlogParms>
    {
    }
}