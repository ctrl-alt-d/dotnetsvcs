using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions.PreConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Artifacts;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Implementations.PreConditions;
public class CreateBlogPreConditions : ICreateBlogPreConditions {
    public CreateBlogPreConditions(IDuplicateTitle duplicateTitle) {
        DuplicateTitle=duplicateTitle;
    }

    protected virtual IDuplicateTitle DuplicateTitle  { get;}

    public async Task Check(
        CreateBlogParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {

        await DuplicateTitle.Check(parms, dbCtxWrapper, cancellationToken);
    }
}
