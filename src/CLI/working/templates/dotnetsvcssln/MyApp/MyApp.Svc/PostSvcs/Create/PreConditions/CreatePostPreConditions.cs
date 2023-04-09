using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoParm.PostParm.Create;
using MyApp.Svcs.Abstractions.PostSvcs.Create.PreConditions;

namespace MyApp.Svcs.PostSvcs.Create.PreConditions;
public class CreatePostPreConditions : ICreatePostPreConditions
{
    public async Task Check(
        CreatePostParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    public void Dispose() {
    }

}
