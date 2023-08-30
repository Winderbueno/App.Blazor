using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
        => RuleSet("Address", () =>
        {
            RuleFor(x => x.ZipCode).NotEmpty();
            RuleFor(x => x.Town).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.Number).NotEmpty();
        });
}
