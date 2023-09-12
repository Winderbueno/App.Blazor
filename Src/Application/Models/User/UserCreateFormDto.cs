using Domain.Enums.User;

namespace Application.Models.User;

public class UserCreateFormDto
{
    // User
    public string? Username { get; set; }
    public UserRole? Role { get; set; }

    // Identity
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordConfirm { get; set; }
}