// Ignore Spelling: Validator

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
            .GreaterThanOrEqualTo(0);
    }
}