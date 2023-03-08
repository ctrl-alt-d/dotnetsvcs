using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create;

public class CreateBlogService : DbOpCreate<Blog, CreateBlogParms>, ICreateBlogService
{
    public CreateBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ICreateBlogServicePreConditions preConditions,
        ICreateBlogServicePostConditions postConditions
        )
        : base(dbCtxWrapperFactory, preConditions, postConditions)
    {
    }

    protected override async Task<Blog> CreateEntityFromParms(CreateBlogParms parms, CancellationToken cancellationToken = default)
    {
        var blog = new Blog()
        {
            Categoria = null,
            EsVisible = true,
            Rating = parms.Rating,
            Titol = parms.Titol
        };

        await Task.CompletedTask;

        return blog;
    }

    protected override Task PostActions(Blog entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    protected override Task PreActions(CreateBlogParms parms, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
