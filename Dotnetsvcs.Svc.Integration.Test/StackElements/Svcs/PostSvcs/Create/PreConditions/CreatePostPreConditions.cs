using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.PostParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.PostSvcs.Create.PreConditions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.PreConditions;
public class CreatePostPreConditions : ICreatePostPreConditions {
    public async Task Check(
        CreatePostParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken) {
        await Task.CompletedTask;
    }
}
