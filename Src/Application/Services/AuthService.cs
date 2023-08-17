using Application.Services.Interfaces;
using Infrastructure.HttpClients.Shop;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IShopApi _shopApi;

    public AuthService(IShopApi shopApi)
        => _shopApi = shopApi;

    public async Task<AuthenticateResponse> SignInAsync()
        => await _shopApi.AuthenticateAsync(
            new AuthenticateRequest { 
                Email = "kevin.gellenoncourt@gmail.com", 
                Password = "patate" 
            });
}