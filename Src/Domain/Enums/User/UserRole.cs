using System.ComponentModel.DataAnnotations;

namespace Domain.Enums.User;

public enum UserRole
{
    [Display(Name = "user.role.admin")]
    Admin = 0,
    [Display(Name = "user.role.user")]
    User,
}