using System.Security.Claims;
using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.Svc;
using Dotnetsvcs.Svc.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
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
    // This service calls post service (*1):
    protected virtual ISvcFactory<ICreatePostService> PostSvcFactory { get; }
    protected virtual IProjectionFactory<IPostDefaultProjection> PostProjectionFactory { get; }
    protected virtual AuthenticationStateProvider AuthenticationStateProvider { get; }
    protected virtual UserManager<IdentityUser> UserManager { get; }
    // Constructor
    public CreateBlogService(
        IDbCtxWrapperFactory dbCtxWrapperFactory,
        ICreateBlogPreConditions preConditions,
        ICreateBlogPostConditions postConditions,
        ISvcFactory<ICreatePostService> svcFactory,
        IProjectionFactory<IPostDefaultProjection> postProjectionFactory,
        IBlogDefaultFilter filter,
        AuthenticationStateProvider authenticationStateProvider,
        UserManager<IdentityUser> userManager
        )
        : base(dbCtxWrapperFactory, preConditions, postConditions, filter)
    {
        PostSvcFactory = svcFactory;
        PostProjectionFactory = postProjectionFactory;
        AuthenticationStateProvider=authenticationStateProvider;
        UserManager = userManager;
    }

    protected override async Task<Blog> CreateEntityFromParms(CreateBlogParms parms, CancellationToken cancellationToken = default)
    {

        var auth = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var claims = auth.User;

        // Opt 1:
        // var user = await UserManager.GetUserAsync( claims )!;
        // DbCtxWrapper.Attach(user);

        // Opt 2:
        var key = claims.FindFirstValue(UserManager.Options.ClaimsIdentity.UserIdClaimType);
        var user = await DbCtxWrapper.FindOrException<IdentityUser>(key);

        var blog = new Blog()
        {
            Category = null,
            IsDeleted = false,
            Rating = parms.Rating,
            Title = parms.Titol,
            Owner = user,
        };

        await Task.CompletedTask;

        return blog;
    }

    protected override async Task PostActions(CreateBlogParms parms, Blog entity, CancellationToken cancellationToken = default)
    {
        using var createPostService = PostSvcFactory.Create();
        using var postProjection = PostProjectionFactory.Create();

        foreach (var i in Enumerable.Range(0, parms.WithNposts))
        {

            var createPostParms = new CreatePostParms()
            {
                BlogKey = new object?[] { entity.Id },
                Descripcio = $"Post test {i}"
            };

            await createPostService.Do(  // (*1)
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
        GC.SuppressFinalize(this);
    }
}
