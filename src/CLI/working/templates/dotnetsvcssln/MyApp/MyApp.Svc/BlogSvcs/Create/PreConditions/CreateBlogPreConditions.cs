using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PreConditions;
using MyApp.Svcs.Abstractions.Common.PreConditions;

namespace MyApp.Svcs.BlogSvcs.Create.PreConditions;
public class CreateBlogPreConditions : ICreateBlogPreConditions
{
    public CreateBlogPreConditions(IDuplicateTitle duplicateTitle, IIsLoggedPreCondition isLoggedPreCondition)
    {
        DuplicateTitle=duplicateTitle;
        IsLoggedPreCondition=isLoggedPreCondition;
    }

    protected virtual IDuplicateTitle DuplicateTitle { get; }
    protected virtual IIsLoggedPreCondition IsLoggedPreCondition { get; }
    

    public async Task Check(
        CreateBlogParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {
        await IsLoggedPreCondition.Check(parms, dbCtxWrapper, cancellationToken);
        await DuplicateTitle.Check(parms, dbCtxWrapper, cancellationToken);
    }
    public void Dispose() {
        DuplicateTitle.Dispose();
        IsLoggedPreCondition.Dispose();
        GC.SuppressFinalize(this);
    }

}
