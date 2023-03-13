using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc;
using Dotnetsvcs.Svc.Abstractions;
using MyApp.DtoParm.BlogParm.Create;
using MyApp.DtoParm.PostParm.Create;
using MyApp.Models;
using MyApp.Projections.Abstractions.PostProjections;
using MyApp.Svcs.Abstractions.BlogSvcs.Common.Filters;
using MyApp.Svcs.Abstractions.BlogSvcs.Create;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PostConditions;
using MyApp.Svcs.Abstractions.BlogSvcs.Create.PreConditions;
using MyApp.Svcs.Abstractions.PostSvcs.Create;

namespace MyApp.Svcs.BlogSvcs.Create;

public class CreateBlogService : DbOpCreate<Blog, CreateBlogParms>, ICreateBlogService
{
    protected virtual ISvcFactory<ICreatePostService> PostCreateSvcFactory { get; }
    protected virtual IProjectionFactory<IPostDefaultProjection> PostProjectorFactory { get; }
    public CreateBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ICreateBlogPreConditions preConditions,
        ICreateBlogPostConditions postConditions,
        ISvcFactory<ICreatePostService> svcFactory,
        IProjectionFactory<IPostDefaultProjection> projectorLocator,
        IBlogDefaultFilter filter
        )
        : base(dbCtxWrapperFactory, preConditions, postConditions, filter)
    {
        PostCreateSvcFactory = svcFactory;
        PostProjectorFactory = projectorLocator;
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
        var createPostService = PostCreateSvcFactory.Create();
        var postProjection = PostProjectorFactory.Create();

        foreach (var i in Enumerable.Range(0, parms.WithNposts))
        {

            var createPostParms = new CreatePostParms()
            {
                BlogKey = new object?[] { entity.Id },
                Descripcio = $"Post test {i}"
            };

            await createPostService.Do(
                createPostParms,
                postProjection,
                DbCtxWrapper,
                cancellationToken);
        }
    }
    protected override Task PreActions(CreateBlogParms parms, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public override void Dispose() {
        base.Dispose();
    }
}
