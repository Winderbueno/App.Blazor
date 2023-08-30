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
        RuleFor(x => x.PhoneNumber).NotEmpty(); // Todo. Do PhoneValidator (Ex. https://stackoverflow.com/questions/12908536/how-to-validate-the-phone-no)
        RuleFor(x => x.PhoneNumberConfirm).Must((model, field) => model.PhoneNumber == field).WithMessage("Does not match");
    }
}