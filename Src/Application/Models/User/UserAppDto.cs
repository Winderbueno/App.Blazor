using Domain.Enums.User;

namespace Application.Models.User;

public class UserAppDto
{
    public int UserId { get; set; }

    public string? Username { get; set; }
    public UserStatus Status { get; set; }
    public UserType Type { get; set; }
    public UserFunction Function { get; set; }
    public IEnumerable<string>? Permissions { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}