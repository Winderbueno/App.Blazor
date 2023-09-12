using Domain.Enums.User;

namespace Application.Dtos.User;

public class UserUpdateFormDto
{
    public int UserId { get; set; }

    public string? Username { get; set; }
    public UserRole? Role { get; set; }
}