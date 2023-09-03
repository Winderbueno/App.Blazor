using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthService _authService;

    public AuthStateProvider(AuthService authService)
        => _authService = authService;

    public async Task LoginAsync(string username, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = await _authService.SignInAsync(username, password);

        if (user is not null)
        {
            principal = user.ToClaimsPrincipal();
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public void Logout()
    {
        _authService.ClearBrowserUserData();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();
        var user = _authService.FetchUserFromBrowser();

        if (user is not null)
        {
            var authUser = await _authService.SignInAsync(user.Username, user.Password);

            if (authUser is not null)
                principal = authUser.ToClaimsPrincipal();
            
        }

        return new(principal);
    }
}