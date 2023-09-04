using Infrastructure.HttpClients.Shop;

namespace Presentation.Middlewares.Authentication;

public class AuthService
{
    private readonly IShopApi _shopApi;

    public AuthService(IShopApi shopApi)
        => _shopApi = shopApi;

    public async Task<User> SignInAsync(string username, string password)
    {
        // Call api to signin
        var resp = await _shopApi.AuthenticateAsync(new()
        {
            Email = username,
            Password = password
        });

        // Build response
        return new()
        {
            Username = resp.UserName,
            Email = resp.Email,
            Password = password,
            Roles = new() { resp.Role },
        };
    }
}