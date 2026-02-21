using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Work;

namespace CRMSystem.Business.Validators.WorkValidator;

public class WorkUpdateRequestValidator : AbstractValidator<WorkUpdateRequest>
{
    public WorkUpdateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .When(x => x.Title is not null)
                .WithMessage("Title should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
            .When(x => x.Title is not null)
                .WithMessage($"Title should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Category)
            .NotEmpty()
            .When(x => x.Category is not null)
                .WithMessage("Category should not be empty")
            .MaximumLength(ValidationConstants.MAX_CATEGORY_LENGTH)
            .When(x => x.Category is not null)
                .WithMessage($"Category should be shorter than {ValidationConstants.MAX_CATEGORY_LENGTH}");

        RuleFor(x => x.Description)
            .NotEmpty()
            .When(x => x.Description is not null)
                .WithMessage("Description should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.StandardTime)
            .GreaterThan(0)
            .When(x => x.StandardTime.HasValue)
                .WithMessage("Standard Time should be positive");

        RuleFor(x => x)
            .Must(x =>
                x.Title is not null
                || x.Category is not null 
                || x.Description is not null 
                || x.StandardTime.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}