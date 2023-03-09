using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Abstractions.PostConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Implementations.PostConditions;

public class CreatePostPostConditions : ICreatePostPostConditions {
    public Task Check(Post entity, CreatePostParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}
