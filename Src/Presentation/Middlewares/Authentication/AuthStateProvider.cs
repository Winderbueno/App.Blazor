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

    private readonly AuthenticationState _anonymous;
    private readonly IAuthService _authService;
    private readonly ILocalStorageService _localStorage;

    public AuthStateProvider(
        IAuthService authService,
        ILocalStorageService localStorage)
    {
        _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        _authService = authService;
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        // Retrieve user from storage
        var storedUser = await GetStoredUser();

        var principal = new ClaimsPrincipal();
        if (storedUser is not null)
        {
            // Todo. Refresh accessToken && Store new one
            principal = storedUser.ToClaimsPrincipal();
        }

        return storedUser is not null ? new(principal) : _anonymous;
    }

    public async Task SignInAsync(string username, string password)
    {
        var user = await _authService.SignInAsync(username, password);

        var principal = new ClaimsPrincipal();
        if (user is not null)
        {
            // Store user (Todo. Store Jwt)
            await SetStoredUser(user);
            principal = user.ToClaimsPrincipal();
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(principal)));
    }

    public async Task SignOut()
    {
        await ClearStoredUser();
        NotifyAuthenticationStateChanged(Task.FromResult(_anonymous));
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
}