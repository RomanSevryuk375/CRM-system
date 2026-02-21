using FluentValidation;
using Shared.Contracts.Position;

namespace CRMSystem.Business.Validators.PositionValidator;

public class PositionRequestValidator : AbstractValidator<PositionRequest>
{
    public PositionRequestValidator()
    {
        RuleFor(x => x.PartId)
            .GreaterThan(0)
                .WithMessage("PartId should be positive");

        RuleFor(x => x.CellId)
            .GreaterThan(0)
                .WithMessage("CellId should be positive");

        RuleFor(x => x.PurchasePrice)
            .GreaterThan(0)
                .WithMessage("Purchase Price should be positive");

        RuleFor(x => x.SellingPrice)
            .GreaterThan(0)
                .WithMessage("Selling Price should be positive");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
                .WithMessage("Quantity should be positive");
    }
}