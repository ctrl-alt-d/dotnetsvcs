using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc.Abstractions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.BlogParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.DtoParm.PostParm.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Models;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Projections.Abstractions.PostProjections;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create.PostConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.BlogSvcs.Create.PreConditions;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Abstractions.PostSvcs.Create;
using Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.Filters.Abstractions.BlogFilters;

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create;

public class CreateBlogService : DbOpCreate<Blog, CreateBlogParms>, ICreateBlogService {
    protected virtual ISvcFactory<ICreatePostService> CreatePostServiceFactory { get; }
    protected virtual IProjectionFactory<IPostDefaultProjection> PostDefaultProjectionFactory { get; }
    public CreateBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ICreateBlogPreConditions preConditions,
        ICreateBlogPostConditions postConditions,
        ISvcFactory<ICreatePostService> createPostServiceFactory,
        IProjectionFactory<IPostDefaultProjection> postDefaultProjectionFactory,
        IBlogDefaultFilter filter
        )
        : base(dbCtxWrapperFactory, preConditions, postConditions, filter) {
        CreatePostServiceFactory = createPostServiceFactory;
        PostDefaultProjectionFactory = postDefaultProjectionFactory;
    }

    protected override async Task<Blog> CreateEntityFromParms(CreateBlogParms parms, CancellationToken cancellationToken = default) {
        var blog = new Blog() {
            Categoria = null,
            EsVisible = true,
            Rating = parms.Rating,
            Titol = parms.Titol
        };

        await Task.CompletedTask;

        return blog;
    }

    protected override async Task PostActions(CreateBlogParms parms, Blog entity, CancellationToken cancellationToken = default) {
        var createPostService = CreatePostServiceFactory.Create();
        var postProjection = PostDefaultProjectionFactory.Create();

        foreach (var i in Enumerable.Range(0, parms.WithNposts)) {

            var createPostParms = new CreatePostParms() {
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



    protected override Task PreActions(CreateBlogParms parms, CancellationToken cancellationToken = default) {
        return Task.CompletedTask;
    }
}
