using Application.Models.Auth;
using Application.Services.Interfaces;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private const string userStorageKey = "user";

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
        // Get stored user
        var storedUser = await GetStoredUser();

        var principal = _anonymous;
        if (storedUser is not null)
        {
            // Refresh AccessToken (Todo. Catch error)
            var user = await _authService.RefreshTokenAsync();
            await SetStoredUser(user);

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
            // Store user
            await SetStoredUser(user);

            // Set AccessToken refresh timer
            SetRefreshTokenTimer();

            principal = user.ToClaimsPrincipal();
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task SignOut()
    {
        await ClearStoredUser();
        if (_refreshTokenTimer is not null) _refreshTokenTimer.Dispose();
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    public async Task<User?> GetStoredUser()
    {
        string json = await _localStorage.GetItemAsync<string>(userStorageKey);
        return json == null ? null : JsonConvert.DeserializeObject<User>(json);
    }

    public async Task SetStoredUser(User user)
        => await _localStorage.SetItemAsync(userStorageKey, JsonConvert.SerializeObject(user));

    public async Task ClearStoredUser()
        => await _localStorage.RemoveItemAsync(userStorageKey);

    private void SetRefreshTokenTimer()
        => _refreshTokenTimer = new Timer(async _ => await RefreshToken(), null, 10000, 10000);

    private async Task RefreshToken()
        => await SetStoredUser(await _authService.RefreshTokenAsync());
}