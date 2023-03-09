using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Exceptions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions.PreConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Artifacts;
using Microsoft.EntityFrameworkCore;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Implementations.PreConditions;
public class DuplicateTitle : IDuplicateTitle
{
    public async Task Check(
        CreateBlogParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {

        var alreadyexists =
            await
            dbCtxWrapper
            .Set<Blog>()
            .Where(x => x.Titol == parms.Titol)
            .AnyAsync(cancellationToken: cancellationToken);

        if (alreadyexists)
            throw new SvcException($"Already exists Blog with title: {parms.Titol}");
    }
}
