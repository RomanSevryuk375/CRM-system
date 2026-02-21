using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Part;

namespace CRMSystem.Business.Validators.PartValidator;

public class PartRequestValidator : AbstractValidator<PartRequest>
{
    public PartRequestValidator()
    {
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

        RuleFor(x => x.ManufacturerArticle)
            .MaximumLength(ValidationConstants.MAX_ARTICLE_LENGTH)
            .When(x => x.ManufacturerArticle is not null)
                .WithMessage($"Manufacturer Article should be shorter than {ValidationConstants.MAX_ARTICLE_LENGTH}");

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

        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.Applicability)
            .NotEmpty()
                .WithMessage("Applicability should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
                .WithMessage($"Applicability should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
    }
}