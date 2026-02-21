using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.PartCategory;

namespace CRMSystem.Business.Validators.PartCategoryValidator;

public class PartCategoryUpdateRequestValidator : AbstractValidator<PartCategoryUpdateRequest>
{
    public PartCategoryUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
            .When(x => x.Name is not null)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x)
            .Must(x => 
                x.Name is not null 
                || x.Description is not null)
            .WithMessage("At least one field should be provided for update");
    }   
}