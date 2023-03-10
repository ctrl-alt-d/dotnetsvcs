using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions.Exceptions;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PreConditions;

namespace MyApp.Svcs.BlogSvcs.Create.PreConditions;
public class DuplicateTitle : IDuplicateTitle
{
    public async Task Check(
        CreateBlogParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {

        var alreadyexists =
            dbCtxWrapper
            .Set<Blog>()
            .Where(x => x.Titol == parms.Titol)
            .Any();  //<-- This can be AnyAsync if adding EF or adding to CtxWrapper (ToDo)

        await Task.CompletedTask;

        if (alreadyexists)
            throw new SvcException($"Already exists Blog with title: {parms.Titol}");
    }
    public void Dispose() {
    }

}
