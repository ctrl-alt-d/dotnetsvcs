using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.PostParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.PostSvcs.Create.PostConditions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.PostConditions;

public class CreatePostPostConditions : ICreatePostPostConditions {
    public Task Check(Post entity, CreatePostParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }
}
