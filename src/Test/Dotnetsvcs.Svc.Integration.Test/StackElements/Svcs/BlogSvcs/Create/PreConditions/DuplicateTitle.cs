using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions.Exceptions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create.PreConditions;
using Microsoft.EntityFrameworkCore;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.PreConditions;
public class DuplicateTitle : IDuplicateTitle {
    public async Task Check(
        CreateBlogParms parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken) {

        var alreadyexists =
            await
            dbCtxWrapper
            .Set<Blog>()
            .Where(x => x.Titol == parms.Titol)
            .AnyAsync(cancellationToken: cancellationToken);

        if (alreadyexists)
            throw new SvcException($"Already exists Blog with title: {parms.Titol}");
    }

    public void Dispose()
    {
    }

}
