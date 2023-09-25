namespace Application.Dtos.Auth;

public class ResetPasswordFormDto
{
    public string? Token { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}