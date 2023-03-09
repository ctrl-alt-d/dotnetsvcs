using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions.PostConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Artifacts;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Implementations.PostConditions;

public class CreateBlogPostConditions : ICreateBlogPostConditions {
    public Task Check(Blog entity, CreateBlogParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}
