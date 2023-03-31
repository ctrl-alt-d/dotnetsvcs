using Dotnetsvcs.DbCtx.Abstractions;
using Dotnetsvcs.DtoParm.Abstractions;
using Dotnetsvcs.Svc.Abstractions.Exceptions;
using Microsoft.AspNetCore.Components.Authorization;
using MyApp.Svcs.Abstractions.Common.PreConditions;

namespace MyApp.Svcs.Common.PreConditions;
public class IsLoggedPreCondition : IIsLoggedPreCondition
{
    public IsLoggedPreCondition(AuthenticationStateProvider authenticationStateProvider)
    {
        AuthenticationStateProvider=authenticationStateProvider;
    }

    protected virtual AuthenticationStateProvider AuthenticationStateProvider { get; }

    public async Task Check(
        IDtoParm parms,
        IDbCtxWrapper dbCtxWrapper,
        CancellationToken cancellationToken)
    {
        var authstate = await AuthenticationStateProvider.GetAuthenticationStateAsync();

        var isAuthenticated = authstate.User?.Identity?.IsAuthenticated ?? false;

        if (!isAuthenticated)
            throw new SvcException($"User not logged in");
    }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
