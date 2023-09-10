﻿using Application.Models.Auth;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Presentation.Middlewares.Authentication;

public class AuthStateProvider : AuthenticationStateProvider
{
    private Timer? _refreshTokenTimer;
    private int refreshTokenDueTime = (int)TimeSpan.FromMinutes(0.5).TotalMilliseconds; // Todo. Take duration from conf

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

        await _tokenService.ClearStored();

        // Dispose refresh token timer
        if (_refreshTokenTimer is not null) _refreshTokenTimer.Dispose();

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    public async Task<ClaimsPrincipal> StartRefreshTokenRotation(User? user = null)
    {
        // Autologin after browser refresh
        var storedToken = await _tokenService.GetStored();
        if (storedToken is not null)
            user = await _authService.RefreshTokenAsync();

        var principal = _anonymous;
        if (user != null)
        {
            // Store accessToken
            await _tokenService.Store(user.AccessToken);

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

    public async Task RefreshAndStoreToken()
        => await _tokenService.Store((await _authService.RefreshTokenAsync()).AccessToken);
}