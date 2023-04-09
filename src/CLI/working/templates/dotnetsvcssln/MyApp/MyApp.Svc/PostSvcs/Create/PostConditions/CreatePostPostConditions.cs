using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoParm.PostParm.Create;
using MyApp.Models;
using MyApp.Svcs.Abstractions.PostSvcs.Create.PostConditions;

namespace MyApp.Svcs.PostSvcs.Create.PostConditions;

public class CreatePostPostConditions : ICreatePostPostConditions
{
    public Task Check(Post entity, CreatePostParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
    public void Dispose() {
    }

}
