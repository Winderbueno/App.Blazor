using Application.Services.Interfaces;
using Blazored.LocalStorage;

namespace Presentation.Middlewares.Authentication;

public class TokenService
{
    private const string tokenStorageKey = "accessToken";
    
    private Timer? _refreshTokenTimer;
    private int refreshTokenDueTime = (int)TimeSpan.FromMinutes(1).TotalMilliseconds; // Todo. Take duration from conf

    private readonly IAuthService _authService;
    private readonly ILocalStorageService _localStorage;

    public TokenService(
        IAuthService authService,
        ILocalStorageService localStorage)
    {
        _authService = authService;
        _localStorage = localStorage;
    }

    public async Task<string?> GetStored()
        => await _localStorage.GetItemAsync<string>(tokenStorageKey);

    public async Task Store(string token)
    {
        // Add token in storage
        await _localStorage.SetItemAsync(tokenStorageKey, token);

        // Set AccessToken refresh timer
        _refreshTokenTimer = new Timer(
            async _ => await RefreshToken(), null,
            refreshTokenDueTime,
            refreshTokenDueTime); ;
    }

    public async Task ClearStored()
    {
        // Clear token from storage
        await _localStorage.RemoveItemAsync(tokenStorageKey);

        // Dispose refresh token timer
        if (_refreshTokenTimer is not null) _refreshTokenTimer.Dispose();
    }

    public async Task RefreshToken()
        => await Store((await _authService.RefreshTokenAsync()).AccessToken);
}