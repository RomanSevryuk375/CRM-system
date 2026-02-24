using FluentValidation;
using Shared.Contracts.PartSet;

namespace CRMSystem.Business.Validators.PartSetValidator;

public class PartSetUpdateRequestValidator : AbstractValidator<PartSetUpdateRequest>
{
    public PartSetUpdateRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .When(x => x.Quantity.HasValue)
                .WithMessage("Quantity should be positive");

        RuleFor(x => x.SoldPrice)
            .GreaterThan(0)
            .When(x => x.SoldPrice.HasValue)
                .WithMessage("Sold Price should be positive");

        RuleFor(x => x)
            .Must(x => 
                x.Quantity.HasValue 
                || x.SoldPrice.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}