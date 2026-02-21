using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Specialization;

namespace CRMSystem.Business.Validators.SpecializationValidator;

public class SpecializationRequestValidator : AbstractValidator<SpecializationRequest>
{
    public SpecializationRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");
    }
}