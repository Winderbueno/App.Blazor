using Application.Dtos.User;
using FluentValidation;

namespace Application.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateFormDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Role).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.PasswordConfirm).Must((model, field) => model.Password == field).WithMessage("Does not match");
    }
}