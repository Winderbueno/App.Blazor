using Application.Dtos.Auth;
using Infrastructure.HttpClients.Shop;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task<User?> SignInAsync(SignInFormDto dto);
    Task<User?> RefreshTokenAsync();
    Task RevokeRefreshTokenAsync();
    Task SignUpAsync(SignUpFormDto dto);
    Task VerifyEmailAsync(string? token);
    Task ForgotPasswordAsync(ForgotPasswordFormDto dto);
    Task ValidateResetTokenAsync(string? token);
    Task ResetPasswordAsync(ResetPasswordFormDto dto);
}