using System.ComponentModel.DataAnnotations;

namespace Domain.Enums.User;

public enum UserStatus
{
    [Display(Name = "user.status.in-progress")]
    InProgress = 0,
    [Display(Name = "user.status.active")]
    Active,
    [Display(Name = "user.status.suspended")]
    Suspended,
    [Display(Name = "user.status.blocked")]
    Blocked
}