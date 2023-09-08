using Application.Models.User;
using FluentValidation;

namespace Application.Validators;

public class UserCreateValidator : AbstractValidator<UserCreateAppDto>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.Type).NotNull();
        RuleFor(x => x.Function).NotEmpty();
        RuleFor(x => x.Username).NotEmpty();

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.EmailConfirm).Must((model, field) => model.Email == field).WithMessage("Does not match");
    }
}