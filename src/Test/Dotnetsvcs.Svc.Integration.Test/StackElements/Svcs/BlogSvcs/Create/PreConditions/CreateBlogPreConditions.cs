using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create.PreConditions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.PreConditions;
public class CreateBlogPreConditions : ICreateBlogPreConditions {
    public CreateBlogPreConditions(IDuplicateTitle duplicateTitle) {
        DuplicateTitle = duplicateTitle;
    }

    protected virtual IDuplicateTitle DuplicateTitle { get; }

    public async Task Check(
        CreateBlogParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken) {

        await DuplicateTitle.Check(parms, dbCtxWrapper, cancellationToken);
    }

    public void Dispose()
    {
    }
}
