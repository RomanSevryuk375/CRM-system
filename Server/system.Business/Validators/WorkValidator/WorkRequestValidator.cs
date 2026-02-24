using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Work;

namespace CRMSystem.Business.Validators.WorkValidator;

public class WorkRequestValidator : AbstractValidator<WorkRequest>
{
    public WorkRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
                .WithMessage("Title should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Title should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Category)
            .NotEmpty()
                .WithMessage("Category should not be empty")
            .MaximumLength(ValidationConstants.MAX_CATEGORY_LENGTH)
                .WithMessage($"Category should be shorter than {ValidationConstants.MAX_CATEGORY_LENGTH}");

        RuleFor(x => x.Description)
            .NotEmpty()
                .WithMessage("Description should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.StandardTime)
            .GreaterThan(0)
                .WithMessage("Standard Time should be positive");
    }
}