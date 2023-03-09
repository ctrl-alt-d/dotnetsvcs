using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Abstractions.PreConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Implementations.PreConditions;
public class CreatePostPreConditions : ICreatePostPreConditions {
    public async Task Check(
        CreatePostParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
