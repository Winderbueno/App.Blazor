using Domain.Enums.User;

namespace Application.Models.User;

public class UserAppDto
{
    public int UserId { get; set; }

    public string? Username { get; set; }
    public UserStatus Status { get; set; }
    public UserRole Role { get; set; }
    public IEnumerable<string>? Permissions { get; set; }

    public string? Email { get; set; }
}