using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Specialization;

namespace CRMSystem.Business.Validators.SpecializationValidator;

public class SpecializationUpdateRequestValidator : AbstractValidator<SpecializationUpdateRequest>
{
    public SpecializationUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
            .When(x => x.Name is not null)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x)
            .Must(x => x.Name is not null)
                .WithMessage("Name field should be provided for update");
    }
}