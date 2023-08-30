using Domain.Enums.User;

namespace Application.Models.User;

public class UserUpdateAppDto
{
    public int UserId { get; set; }

    public UserType? Type { get; set; }
    public UserFunction? Function { get; set; }
}