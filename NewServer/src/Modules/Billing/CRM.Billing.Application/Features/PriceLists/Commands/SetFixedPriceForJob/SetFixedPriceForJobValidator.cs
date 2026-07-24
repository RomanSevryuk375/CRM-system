// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.PriceLists.Commands.SetFixedPriceForJob;

public sealed class SetFixedPriceForJobValidator
    : AbstractValidator<SetFixedPriceForJobCommand>
{
    public SetFixedPriceForJobValidator()
    {
        RuleFor(x => x.PriceListId)
            .NotEmpty();

        RuleFor(x => x.JobId)
            .NotEmpty();

        RuleFor(x => x.FixedPrice)
            .GreaterThanOrEqualTo(0);
    }
}