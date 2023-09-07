using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace Infrastructure.HttpHandlers;

public class AddTokenHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;

    public AddTokenHandler(ILocalStorageService localStorage) => _localStorage = localStorage;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage req, CancellationToken ct)
    {
        string token = await _localStorage.GetItemAsync<string>("accessToken");
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(req, ct);
    }
}