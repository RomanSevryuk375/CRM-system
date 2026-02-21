using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Position;

namespace CRMSystem.Business.Validators.PositionValidator;

public class PositionWithPartRequestValidator : AbstractValidator<PositionWithPartRequest>
{
    public PositionWithPartRequestValidator()
    {
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
        
        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
                .WithMessage("CategoryId should be positive");

        RuleFor(x => x.InternalArticle)
            .NotEmpty()
                .WithMessage("Internal Article should not be empty")
            .MaximumLength(ValidationConstants.MAX_ARTICLE_LENGTH)
                .WithMessage($"Internal Article should be shorter than {ValidationConstants.MAX_ARTICLE_LENGTH}");

        RuleFor(x => x.OemArticle)
            .MaximumLength(ValidationConstants.MAX_ARTICLE_LENGTH)
            .When(x => x.OemArticle is not null)
                .WithMessage($"OEM Article should be shorter than {ValidationConstants.MAX_ARTICLE_LENGTH}");

        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Manufacturer)
            .NotEmpty()
                .WithMessage("Manufacturer should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Manufacturer should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Applicability)
            .NotEmpty()
                .WithMessage("Applicability should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
                .WithMessage($"Applicability should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
    }
}