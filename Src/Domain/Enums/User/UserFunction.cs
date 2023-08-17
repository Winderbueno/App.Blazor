using System.ComponentModel.DataAnnotations;

namespace Domain.Enums.User;

public enum UserFunction
{
    [Display(Name = "user.function.manager")]
    Manager = 0,
    [Display(Name = "user.function.employee")]
    Employee,
    [Display(Name = "user.function.support")]
    Support
}