using Application.Models.Auth;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private Timer? _refreshTokenTimer;
    private int refreshTokenDueTime = (int)TimeSpan.FromMinutes(15).TotalMilliseconds; // Todo. Take duration from conf

    private readonly ClaimsPrincipal _anonymous;
    private readonly IAuthService _authService;
    private readonly TokenStorage _tokenStorage;

    public AuthStateProvider(
        IAuthService authService,
        TokenStorage tokenStorage)
    {
        _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        _authService = authService;
        _tokenStorage = tokenStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        => new(await StartRefreshTokenRotation());

    public async Task SignInAsync(string username, string password)
    {
        var user = await _authService.SignInAsync(username, password);
        var principal = await StartRefreshTokenRotation(user);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task SignOut()
    {
        await _authService.RevokeRefreshTokenAsync();
        await StopRefreshTokenRotation();
    }

    private async Task<ClaimsPrincipal> StartRefreshTokenRotation(User? user = null)
    {
        // Autologin after browser refresh
        if (await _tokenStorage.Get() is not null)
            user = await RefreshToken();

        var principal = _anonymous;
        if (user != null)
        {
            // Store accessToken
            await _tokenStorage.Store(user.AccessToken);

            // Set accessToken refresh timer
            _refreshTokenTimer = new Timer(
                async _ => await RefreshAndStoreToken(), null,
                refreshTokenDueTime,
                refreshTokenDueTime);

            // Set principal claim with user
            principal = user.ToClaimsPrincipal();
        }

        return principal;
    }

    private async Task StopRefreshTokenRotation()
    {
        await _tokenStorage.Clear();

        // Dispose refresh token timer
        if (_refreshTokenTimer is not null) _refreshTokenTimer.Dispose();

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private async Task RefreshAndStoreToken()
    {
        var user = await RefreshToken();
        if (user is not null) await _tokenStorage.Store(user.AccessToken);
    }

    private async Task<User?> RefreshToken()
    {
        User? user = null;
        try { user = await _authService.RefreshTokenAsync(); } 
        catch { await StopRefreshTokenRotation(); }
        return user!;
    }
}