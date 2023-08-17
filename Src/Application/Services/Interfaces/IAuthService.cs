using Infrastructure.HttpClients.Shop;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task<AuthenticateResponse> SignInAsync();
}