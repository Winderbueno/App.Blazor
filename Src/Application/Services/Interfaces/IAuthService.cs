using Application.Dtos.Auth;

namespace Application.Services.Interfaces;

public interface IAuthService
{
    Task<User?> SignInAsync(SignInFormDto dto);
    Task<User?> RefreshTokenAsync();
    Task RevokeRefreshTokenAsync();
    Task SignUpAsync(SignUpFormDto dto);
    Task ForgotPasswordAsync(ForgotPasswordFormDto dto);
    Task ValidateResetTokenAsync(string? token);
    Task ResetPasswordAsync(ResetPasswordFormDto dto);
}