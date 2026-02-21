using FluentValidation;
using Shared.Contracts.SupplySet;

namespace CRMSystem.Business.Validators.SupplySetValidator;

public class SupplySetUpdateRequestValidator : AbstractValidator<SupplySetUpdateRequest>
{
    public SupplySetUpdateRequestValidator()
    {
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .When(x => x.Quantity.HasValue)
                .WithMessage("Quantity should be positive");

        RuleFor(x => x.PurchasePrice)
            .GreaterThan(0)
            .When(x => x.PurchasePrice.HasValue)
                .WithMessage("Purchase Price should be positive");

        RuleFor(x => x)
            .Must(x =>
                x.Quantity.HasValue 
                || x.PurchasePrice.HasValue)
                .WithMessage("At least one field should be provided for update");
    }
}