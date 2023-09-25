using Application.Dtos.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordFormDto>
{
    public ForgotPasswordValidator()
        => RuleFor(x => x.Email).NotEmpty().EmailAddress();
}