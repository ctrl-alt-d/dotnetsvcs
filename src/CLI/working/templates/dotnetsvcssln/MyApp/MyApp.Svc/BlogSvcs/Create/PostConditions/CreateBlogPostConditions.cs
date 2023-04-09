using Dotnetsvcs.DbCtx.Abstractions;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PostConditions;

namespace MyApp.Svcs.BlogSvcs.Create.PostConditions;

public class CreateBlogPostConditions : ICreateBlogPostConditions
{
    public Task Check(Blog entity, CreateBlogParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose() {
    }
}
