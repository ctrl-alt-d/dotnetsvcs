using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions.Exceptions;
using MyApp.DtoParm.BlogParm.Delete;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Common.Filters;
using MyApp.Svcs.Abstractions.BlogSvcs.Delete.PreConditions;

namespace MyApp.Svcs.BlogSvcs.Delete.PreConditions;

public class DeleteBlogPreConditions : IDeleteBlogPreConditions
{
    protected readonly IBlogDefaultFilter Filter;

    public DeleteBlogPreConditions(IBlogDefaultFilter filter)
    {
        Filter=filter;
    }

    public async Task Check(DeleteBlogParms parms, IDbCtxWrapper dbCtxWrapper, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var dbmodel =
            await
            dbCtxWrapper
            .FindAsync<Blog>(parms.KeyValues);

        var filter = await Filter.GetFilter(dbCtxWrapper);
        var f = filter.Compile();

        var notExists =
            dbmodel == null || !f(dbmodel)  || dbmodel.IsDeleted;


        if (notExists)
            throw new SvcException($"This model doesn't exist or it is already deleted");
    }

    public void Dispose()
    {
        Filter.Dispose();
        GC.SuppressFinalize(this);
    }
}
