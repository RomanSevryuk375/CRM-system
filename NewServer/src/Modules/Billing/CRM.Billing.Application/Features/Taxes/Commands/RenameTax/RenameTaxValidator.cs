// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Taxes.Commands.RenameTax;

public sealed class RenameTaxValidator
    : AbstractValidator<RenameTaxCommand>
{
    public RenameTaxValidator()
    {
        RuleFor(x => x.TaxId).NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(64);
    }
}