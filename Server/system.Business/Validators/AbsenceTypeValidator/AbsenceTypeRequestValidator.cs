using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.AbsenceType;

namespace CRMSystem.Business.Validators.AbsenceTypeValidator;

public class AbsenceTypeRequestValidator : AbstractValidator<AbsenceTypeRequest>
{
    public AbsenceTypeRequestValidator()
    {
        RuleFor(x => x.Name)
            .Length(ValidationConstants.MAX_TYPE_NAME)
                .WithMessage($"Name should be shoter than {ValidationConstants.MAX_TYPE_NAME}.")
            .NotEmpty().NotNull()
                .WithMessage("Name should not be empty");
    }
}