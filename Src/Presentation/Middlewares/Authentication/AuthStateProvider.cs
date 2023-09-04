using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly AuthenticationState _anonymous;
    private readonly AuthService _authService;
    private readonly ILocalStorageService _localStorage;

    public AuthStateProvider(
        AuthService authService,
        ILocalStorageService localStorage)
    {
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        _authService = authService;
        _localStorage = localStorage;
    }

    public async Task SignInAsync(string username, string password)
    {
        var principal = new ClaimsPrincipal();
        var user = await _authService.SignInAsync(username, password);

        if (user is not null) principal = user.ToClaimsPrincipal();

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public void SignOut()
    {
        _authService.ClearBrowserUserData();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }


    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        ClaimsPrincipal principal = new(new ClaimsIdentity());

        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrWhiteSpace(token)) 
            return _anonymous;

        var localUser = _authService.FetchUserFromBrowser();
        if (localUser is not null)
        {
            var authUser = await _authService.SignInAsync(localUser.Username, localUser.Password);

            if (authUser is not null) principal = authUser.ToClaimsPrincipal();
        }

        return new(principal);
    }
}