// Ignore Spelling: Validator

using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentValidation;

namespace CRM.Billing.Application.Features.Taxes.Commands.UpdateTaxRate;

public sealed class UpdateTaxRateValidator
    : AbstractValidator<UpdateTaxRateCommand>
{
    public UpdateTaxRateValidator()
    {
        RuleFor(x => x.TaxId)
            .NotEmpty();

        RuleFor(x => x.Rate)
            .GreaterThanOrEqualTo(TaxRate.MinPercentage)
            .LessThanOrEqualTo(TaxRate.MaxPercentage);
    }
}