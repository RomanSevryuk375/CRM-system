// Ignore Spelling: Validator

using CRM.Billing.Domain.Entities;
using FluentValidation;

namespace CRM.Billing.Application.Features.Taxes.Commands.CreateTax;

public sealed class CreateTaxValidator
    : AbstractValidator<CreateTaxCommand>
{
    public CreateTaxValidator()
    {
        RuleFor(x => x.TaxId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(Tax.MaxNameLength);

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TypeId)
            .IsInEnum();
    }
}