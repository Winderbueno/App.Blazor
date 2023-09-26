using Application.Dtos.Auth;
using FluentValidation;

namespace Application.Validators.Auth;

public class SignUpValidator : AbstractValidator<SignUpFormDto>
{
    public SignUpValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.PasswordConfirm).Must((model, field) => model.Password == field).WithMessage("Does not match");
        RuleFor(x => x.AcceptTerms).Equal(true);
    }
}