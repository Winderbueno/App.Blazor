using Domain.Enums.User;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.User;

public class UserCreateAppDto
{
    [Required]
    public UserType Type { get; set; } = UserType.Internal;

    [Required]
    public UserFunction? Function { get; set; }

    public string? Username { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "{0} is too long.")]
    public string? FirstName { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "{0} is too long.")]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Field should have an email format")]
    public string? Email { get; set; }

    [Required]
    [Display(Name = "Confirm Email")]
    [Compare("Email")]
    public string? EmailConfirm { get; set; }

    [Required]
    [Display(Name = "Phone Number")]
    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    [Display(Name = "Confirm Phone Number")]
    [Compare("PhoneNumber")]
    public string? PhoneNumberConfirm { get; set; }
}