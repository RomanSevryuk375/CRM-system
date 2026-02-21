using FluentValidation;
using Shared.Contracts.Absence;

namespace CRMSystem.Business.Validators.AbsenceValidator;

public class AbsenceUpdateRequestValidator : AbstractValidator<AbsenceUpdateRequest>
{
    public AbsenceUpdateRequestValidator()
    {
        RuleFor(x => x.TypeId)
            .IsInEnum()
            .When(x => x.TypeId.HasValue)
                .WithMessage("TypeId should be a valid enum value");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .When(x => x.StartDate.HasValue)
                .WithMessage("StartDate should not be empty");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate!.Value)
            .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("EndDate should be greater than StartDate");

        RuleFor(x => x)
            .Must(x => 
                x.TypeId.HasValue 
                || x.StartDate.HasValue 
                || x.EndDate.HasValue)
            .WithMessage("At least one field should be provided for update");
    }   
}