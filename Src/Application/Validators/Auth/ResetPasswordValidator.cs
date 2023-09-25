using Application.Dtos.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordFormDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.PasswordConfirm).Must((model, field) => model.Password == field).WithMessage("Does not match");
    }
}