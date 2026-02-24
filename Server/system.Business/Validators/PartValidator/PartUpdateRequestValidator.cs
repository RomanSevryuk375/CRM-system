using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Part;

namespace CRMSystem.Business.Validators.PartValidator;

public class PartUpdateRequestValidator : AbstractValidator<PartUpdateRequest>
{
    public PartUpdateRequestValidator()
    {
        RuleFor(x => x.InternalArticle)
            .NotEmpty()
            .When(x => x.InternalArticle is not null)
                .WithMessage("Internal Article should not be empty")
            .MaximumLength(ValidationConstants.MAX_ARTICLE_LENGTH)
                .When(x => x.InternalArticle is not null)
                .WithMessage($"Internal Article should be shorter than {ValidationConstants.MAX_ARTICLE_LENGTH}");

        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
            .When(x => x.Name is not null)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Manufacturer)
            .NotEmpty()
            .When(x => x.Manufacturer is not null)
                .WithMessage("Manufacturer should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
            .When(x => x.Manufacturer is not null)
                .WithMessage($"Manufacturer should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Applicability)
            .NotEmpty()
            .When(x => x.Applicability is not null)
                .WithMessage("Applicability should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Applicability is not null)
                .WithMessage($"Applicability should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x)
            .Must(x => 
                x.InternalArticle is not null 
                || x.Name is not null 
                || x.Manufacturer is not null 
                || x.Applicability is not null 
                || x.OemArticle is not null 
                || x.ManufacturerArticle is not null 
                || x.Description is not null)
            .WithMessage("At least one field should be provided for update");
    }
}