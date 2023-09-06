using Application.Models.Auth;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task<User> SignInAsync(string username, string password);
    Task<User> RefreshAccessTokenAsync();
}