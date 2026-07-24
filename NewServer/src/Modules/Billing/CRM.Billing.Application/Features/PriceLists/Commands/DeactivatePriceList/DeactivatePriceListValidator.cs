// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.PriceLists.Commands.DeactivatePriceList;

public sealed class DeactivatePriceListValidator
    : AbstractValidator<DeactivatePriceListCommand>
{
    public DeactivatePriceListValidator()
    {
        RuleFor(x => x.PriceListId)
            .NotEmpty();

        RuleFor(x => x.ValidTo)
            .NotEmpty();
    }
}