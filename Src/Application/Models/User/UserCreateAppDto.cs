using Domain.Enums.User;

namespace Application.Models.User;

public class UserCreateAppDto
{
    public UserType Type { get; set; } = UserType.Internal;
    public UserFunction? Function { get; set; }
    public string? Username { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? EmailConfirm { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneNumberConfirm { get; set; }
}