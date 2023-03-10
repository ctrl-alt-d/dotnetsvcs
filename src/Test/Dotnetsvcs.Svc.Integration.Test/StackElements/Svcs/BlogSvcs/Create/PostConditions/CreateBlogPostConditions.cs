using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create.PostConditions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.PostConditions;

public class CreateBlogPostConditions : ICreateBlogPostConditions {
    public Task Check(Blog entity, CreateBlogParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
    }
}
