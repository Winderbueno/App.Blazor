using System.ComponentModel.DataAnnotations;

namespace Domain.Enums.User;

public enum UserType
{
    [Display(Name = "user.type.internal")]
    Internal = 0,
    [Display(Name = "user.type.external")]
    External,
}