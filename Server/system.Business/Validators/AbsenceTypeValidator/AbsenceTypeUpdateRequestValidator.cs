using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.AbsenceType;

namespace CRMSystem.Business.Validators.AbsenceTypeValidator;

public class AbsenceTypeUpdateRequestValidator : AbstractValidator<AbsenceTypeUpdateRequest>
{
    public AbsenceTypeUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");
    }
}