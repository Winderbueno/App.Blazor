using Application.Models.User;
using FluentValidation;

namespace Application.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateAppDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Role).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.PasswordConfirm).Must((model, field) => model.Email == field).WithMessage("Does not match");
    }
}