using Dotnetsvcs.DbCtx.Abstractions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using MyApp.Models;
using MyApp.Svcs.Abstractions.BlogSvcs.Common.Filters;
using System.Linq.Expressions;
using System.Security.Claims;

namespace MyApp.Svcs.BlogSvcs.Common.Filters;

public class BlogDefaultFilter : IBlogDefaultFilter
{
    public BlogDefaultFilter(UserManager<IdentityUser> userManager, AuthenticationStateProvider authenticationStateProvider)
    {
        UserManager=userManager;
        AuthenticationStateProvider=authenticationStateProvider;
    }

    protected virtual UserManager<IdentityUser> UserManager { get; }

    protected virtual AuthenticationStateProvider AuthenticationStateProvider { get; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async Task<Expression<Func<Blog, bool>>> GetFilter(IDbCtxWrapper ctx)
    {

        var auth = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var claims = auth.User;
        var key = claims.FindFirstValue(UserManager.Options.ClaimsIdentity.UserIdClaimType);
        var currentUser = await ctx.FindAsync<IdentityUser>(key);

        // Blog not deleted AND logged user is blog's owner
        return blog => !blog.IsDeleted && blog.Owner == currentUser;
    }
}
