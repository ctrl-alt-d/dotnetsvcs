using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc;
using MyApp.DtoParm.BlogParm.Delete;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Common.Filters;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PostConditions;
using MyApp.Svcs.Abstractions.BlogSvcs.Delete;
using MyApp.Svcs.Abstractions.BlogSvcs.Delete.PreConditions;

namespace MyApp.Svcs.BlogSvcs.Delete;

public class DeleteBlogService : DbOpSoftDelete<Blog, DeleteBlogParms>, IDeleteBlogService
{
    public DeleteBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        IDeleteBlogPreConditions preCondition, 
        IDeleteBlogPostConditions postCondition, 
        IBlogDefaultFilter filter) : base(dbCtxWrapperFactory, preCondition, postCondition, filter)
    {
    }

    public override void Dispose() {
        base.Dispose();
    }

    protected override Task PostActions(DeleteBlogParms parms, Blog entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    protected override Task PreActions(DeleteBlogParms parms, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    protected override Task<Blog> SoftDeleteModel(DeleteBlogParms parms, Blog entity, CancellationToken cancellationToken = default)
    {
        entity.IsDeleted = true;
        return Task.FromResult(entity);
    }
}
