using System.ComponentModel.DataAnnotations;

namespace Domain.Enums.User;

public enum UserStatus
{
    [Display(Name = "user.status.active")]
    Active = 0,
    [Display(Name = "user.status.in-progress")]
    InProgress,
    [Display(Name = "user.status.suspended")]
    Suspended
}