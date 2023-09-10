using Blazored.LocalStorage;

namespace Presentation.Middlewares.Authentication;

public class TokenService
{
    private const string tokenStorageKey = "accessToken";
    private readonly ILocalStorageService _localStorage;

    public TokenService(ILocalStorageService localStorage)
        => _localStorage = localStorage;

    public async Task<string?> GetStored()
        => await _localStorage.GetItemAsync<string>(tokenStorageKey);

    public async Task Store(string token)
        => await _localStorage.SetItemAsync(tokenStorageKey, token);

    public async Task ClearStored()
        => await _localStorage.RemoveItemAsync(tokenStorageKey);
}