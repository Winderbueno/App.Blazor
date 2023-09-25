using Application.Dtos.Auth;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task<User?> SignInAsync(string username, string password);
    Task<User?> RefreshTokenAsync();
    Task RevokeRefreshTokenAsync();
    Task ForgotPasswordAsync(ForgotPasswordFormDto dto);
    Task ValidateResetTokenAsync(string? token);
    Task ResetPasswordAsync(ResetPasswordFormDto dto);
}