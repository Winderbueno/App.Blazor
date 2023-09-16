using Domain.Attributes;
using K.Blazor.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Enums.User;

public enum UserStatus
{
    [Display(Name = "user.status.active")]
    [Color(Color.Success)]
    Active = 0,
    [Display(Name = "user.status.in-progress")]
    [Color(Color.Warning)]
    InProgress,
    [Display(Name = "user.status.suspended")]
    [Color(Color.Disabled)]
    Suspended
}