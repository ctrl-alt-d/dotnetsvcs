using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions.PostConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Abstractions.PreConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Artifacts;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.PostSvcs.Create.Artifacts;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create.Implementations;

public class CreateBlogService : DbOpCreate<Blog, CreateBlogParms>, ICreateBlogService
{
    protected virtual ISvcLocator SvcLocator { get; }
    public CreateBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ICreateBlogPreConditions preConditions,
        ICreateBlogPostConditions postConditions        ,
        ISvcLocator svcLocator
        )
        : base(dbCtxWrapperFactory, preConditions, postConditions)
    {
        SvcLocator = svcLocator;
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

    protected override async Task PostActions(CreateBlogParms parms, Blog entity, CancellationToken cancellationToken = default)
    {
        var createPostService = SvcLocator.LocateSvc<ICreatePostService>();

        foreach( var i in Enumerable.Range(0, parms.WithNposts)) {

            var createPostParms = new CreatePostParms() {
                BlogKey = new object?[] { entity.Id },
                Descripcio = $"Post test {i}"
            };

            await createPostService.Do(
                createPostParms,
                PostDefaultProjection.ToDtoResult,
                DbCtxWrapper,
                cancellationToken);

        }
    }

    

    protected override Task PreActions(CreateBlogParms parms, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
