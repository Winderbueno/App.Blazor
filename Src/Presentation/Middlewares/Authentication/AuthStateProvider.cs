using Application.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ClaimsPrincipal _anonymous;
    private readonly IAuthService _authService;
    private readonly TokenService _tokenService;

    public AuthStateProvider(
        IAuthService authService,
        TokenService tokenService)
    {
        _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        _authService = authService;
        _tokenService = tokenService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Get token from storage
        var token = await _tokenService.GetStored();

        var principal = _anonymous;
        if (token is not null)
        {
            // Refresh AccessToken (Todo. Catch error)
            var user = await _authService.RefreshTokenAsync();

            await _tokenService.Store(user.AccessToken);

            if(user != null) principal = user.ToClaimsPrincipal();
        }

        return new(principal);
    }

    public async Task SignInAsync(string username, string password)
    {
        var user = await _authService.SignInAsync(username, password);

        var principal = _anonymous;
        if (user is not null)
        {
            // Store token & Start refresh timer
            await _tokenService.Store(user.AccessToken);

            // Set principal claim with user
            principal = user.ToClaimsPrincipal();
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task SignOut()
    {
        await _tokenService.ClearStored();

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }
}