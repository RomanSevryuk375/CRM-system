// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.PriceLists.Commands.MakePriceListDefault;

public sealed class MakePriceListDefaultValidator
    : AbstractValidator<MakePriceListDefaultCommand>
{
    public MakePriceListDefaultValidator()
    {
        RuleFor(x => x.PriceListId)
            .NotEmpty();
    }
}