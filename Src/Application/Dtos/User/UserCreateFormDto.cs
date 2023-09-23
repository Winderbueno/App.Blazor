using Domain.Enums.User;

namespace Application.Dtos.User;

public class UserCreateFormDto
{
    // User
    public string? Username { get; set; }
    public UserRole? Role { get; set; } = UserRole.User;

    // Identity
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}