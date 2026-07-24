// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Taxes.Commands.DeleteTax;

public sealed class DeleteTaxValidator
    : AbstractValidator<DeleteTaxCommand>
{
    public DeleteTaxValidator()
    {
        RuleFor(x => x.TaxId)
            .NotEmpty();
    }
}