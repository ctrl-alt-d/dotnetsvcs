using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PreConditions;

namespace MyApp.Svcs.BlogSvcs.Create.PreConditions;
public class CreateBlogPreConditions : ICreateBlogPreConditions
{
    public CreateBlogPreConditions(IDuplicateTitle duplicateTitle)
    {
        DuplicateTitle = duplicateTitle;
    }

    protected virtual IDuplicateTitle DuplicateTitle { get; }

    public async Task Check(
        CreateBlogParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {

        await DuplicateTitle.Check(parms, dbCtxWrapper, cancellationToken);
    }
    public void Dispose() {
    }

}
