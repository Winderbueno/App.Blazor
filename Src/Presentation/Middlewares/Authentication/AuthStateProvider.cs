using Application.Services.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private const string tokenStorageKey = "accessToken";

    private Timer? _refreshTokenTimer;
    private readonly ClaimsPrincipal _anonymous;
    private readonly IAuthService _authService;
    private readonly ILocalStorageService _localStorage;

    public AuthStateProvider(
        IAuthService authService,
        ILocalStorageService localStorage)
    {
        _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        _authService = authService;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Get token from storage
        var token = await GetStoredToken();

        var principal = _anonymous;
        if (token is not null)
        {
            // Refresh AccessToken (Todo. Catch error)
            var user = await _authService.RefreshTokenAsync();
            await StoreToken(user.AccessToken);

            // Set AccessToken refresh timer
            SetRefreshTokenTimer();

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
            // Store token
            await StoreToken(user.AccessToken);

            // Set AccessToken refresh timer
            SetRefreshTokenTimer();

            principal = user.ToClaimsPrincipal();
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task SignOut()
    {
        await ClearStoredToken();
        if (_refreshTokenTimer is not null) _refreshTokenTimer.Dispose();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    public async Task<string?> GetStoredToken()
        => await _localStorage.GetItemAsync<string>(tokenStorageKey);

    public async Task StoreToken(string token)
        => await _localStorage.SetItemAsync(tokenStorageKey, token);

    public async Task ClearStoredToken()
        => await _localStorage.RemoveItemAsync(tokenStorageKey);

    private void SetRefreshTokenTimer()
        => _refreshTokenTimer = new Timer(async _ => await RefreshToken(), null, 100000, 100000); // Todo. Set timer duration in var & chg value

    private async Task RefreshToken()
        => await StoreToken((await _authService.RefreshTokenAsync()).AccessToken);
}