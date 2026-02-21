using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.PartCategory;

namespace CRMSystem.Business.Validators.PartCategoryValidator;

public class PartCategoryRequestValidator : AbstractValidator<PartCategoryRequest>
{
    public PartCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
    }
}