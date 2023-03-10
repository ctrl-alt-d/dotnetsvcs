using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc;
using MyApp.DtoParm.PostParm.Create;
using MyApp.Models;
using MyApp.Svcs.Abstractions.PostSvcs.Create;
using MyApp.Svcs.Abstractions.PostSvcs.Create.PostConditions;
using MyApp.Svcs.Abstractions.PostSvcs.Create.PreConditions;

namespace MyApp.Svcs.PostSvcs.Create;

public class CreatePostService : DbOpCreate<Post, CreatePostParms>, ICreatePostService
{
    public CreatePostService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ICreatePostPreConditions preConditions,
        ICreatePostPostConditions postConditions
        )
        : base(dbCtxWrapperFactory, preConditions, postConditions)
    {
    }

    protected override async Task<Post> CreateEntityFromParms(CreatePostParms parms, CancellationToken cancellationToken = default)
    {
        var Post = new Post()
        {
            Descripcio = parms.Descripcio,
            Blog = await DbCtxWrapper.FindOrException<Blog>(parms.BlogKey)
        };

        await Task.CompletedTask;

        return Post;
    }

    protected override Task PostActions(CreatePostParms parms, Post entity, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    protected override Task PreActions(CreatePostParms parms, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
