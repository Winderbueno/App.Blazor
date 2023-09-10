using Blazored.LocalStorage;

namespace Presentation.Middlewares.Authentication;

public class TokenStorage
{
    private const string tokenKey = "accessToken";
    private readonly ILocalStorageService _localStorage;

    public TokenStorage(ILocalStorageService localStorage)
        => _localStorage = localStorage;

    public async Task<string?> Get()
        => await _localStorage.GetItemAsync<string>(tokenKey);

    public async Task Store(string token)
        => await _localStorage.SetItemAsync(tokenKey, token);

    public async Task Clear()
        => await _localStorage.RemoveItemAsync(tokenKey);
}