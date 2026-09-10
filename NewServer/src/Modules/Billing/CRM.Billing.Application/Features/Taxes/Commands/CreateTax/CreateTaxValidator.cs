// Ignore Spelling: Validator

using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.DDD.ValueObjects;
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
            .MaximumLength(Name.MaxLength);

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(TaxRate.MinPercentage)
            .LessThanOrEqualTo(TaxRate.MaxPercentage);

        RuleFor(x => x.TypeId)
            .IsInEnum();
    }
}