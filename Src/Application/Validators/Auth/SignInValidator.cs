using Application.Dtos.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class SignInValidator : AbstractValidator<SignInFormDto>
{
    public SignInValidator()
    {
        RuleFor(x => x.Username).Cascade(CascadeMode.Stop).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}