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

namespace Dotnetsvcs.Svc.Integration.Test.StackElements.Svcs.BlogSvcs.Create;

public class CreateBlogService : DbOpCreate<Blog, CreateBlogParms>, ICreateBlogService {
    protected virtual ISvcLocator SvcLocator { get; }
    protected virtual IProjectorLocator ProjectorLocator { get; }
    public CreateBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ICreateBlogPreConditions preConditions,
        ICreateBlogPostConditions postConditions,
        ISvcLocator svcLocator,
        IProjectorLocator projectorLocator
        )
        : base(dbCtxWrapperFactory, preConditions, postConditions) {
        SvcLocator = svcLocator;
        ProjectorLocator = projectorLocator;
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
        var createPostService = SvcLocator.LocateSvc<ICreatePostService>();
        var postProjection = ProjectorLocator.Locate<IPostDefaultProjection>();

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
