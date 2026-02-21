using FluentValidation;
using Shared.Contracts.Position;

namespace CRMSystem.Business.Validators.PositionValidator;

public class PositionUpdateRequestValidator : AbstractValidator<PositionUpdateRequest>
{
    public PositionUpdateRequestValidator()
    {
        RuleFor(x => x.CellId)
            .GreaterThan(0)
            .When(x => x.CellId.HasValue)
                .WithMessage("CellId should be positive");

        RuleFor(x => x.PurchasePrice)
            .GreaterThan(0)
            .When(x => x.PurchasePrice.HasValue)
                .WithMessage("Purchase Price should be positive");

        RuleFor(x => x.SellingPrice)
            .GreaterThan(0)
            .When(x => x.SellingPrice.HasValue)
                .WithMessage("Selling Price should be positive");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .When(x => x.Quantity.HasValue)
                .WithMessage("Quantity should be positive");

        RuleFor(x => x)
            .Must(x => 
                x.CellId.HasValue 
                || x.PurchasePrice.HasValue 
                || x.SellingPrice.HasValue 
                || x.Quantity.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}