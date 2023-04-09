using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions.Exceptions;
using MyApp.DtoParm.BlogParm.Delete;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Common.Filters;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PostConditions;
using System.Threading.Tasks.Sources;

namespace MyApp.Svcs.BlogSvcs.Delete.PostConditions;

public class DeleteBlogPostConditions : IDeleteBlogPostConditions
{
    public Task Check(Blog entity, DeleteBlogParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
    }
}
