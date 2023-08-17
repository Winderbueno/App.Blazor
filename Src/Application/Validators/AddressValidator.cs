using Application.Models;
using FluentValidation;

namespace Application.Validators;

public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
        => RuleSet("Address", () =>
        {
            RuleFor(x => x.ZipCode).NotEmpty().WithMessage("Le code postal est obligatoire.");
            RuleFor(x => x.Town).NotEmpty().WithMessage("La ville est obligatoire.");
            RuleFor(x => x.Street).NotEmpty().WithMessage("La rue est obligatoire.");
            RuleFor(x => x.Number).NotEmpty().WithMessage("Le numéro est obligatoire.");
        });
}
