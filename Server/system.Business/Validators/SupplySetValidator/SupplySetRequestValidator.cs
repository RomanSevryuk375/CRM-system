using FluentValidation;
using Shared.Contracts.SupplySet;

namespace CRMSystem.Business.Validators.SupplySetValidator;

public class SupplySetRequestValidator : AbstractValidator<SupplySetRequest>
{
    public SupplySetRequestValidator()
    {
        RuleFor(x => x.SupplyId)
            .GreaterThan(0)
                .WithMessage("SupplyId should be positive");

        RuleFor(x => x.PositionId)
            .GreaterThan(0)
                .WithMessage("PositionId should be positive");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
                .WithMessage("Quantity should be positive");

        RuleFor(x => x.PurchasePrice)
            .GreaterThan(0)
                .WithMessage("Purchase Price should be positive");
    }
}