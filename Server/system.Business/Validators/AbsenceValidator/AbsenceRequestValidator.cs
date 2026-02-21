using FluentValidation;
using Shared.Contracts.Absence;

namespace CRMSystem.Business.Validators.AbsenceValidator;

public class AbsenceRequestValidator : AbstractValidator<AbsenceRequest>
{
    public AbsenceRequestValidator()
    {
        RuleFor(x => x.WorkerId)
            .GreaterThan(0)
                .WithMessage("WorkerId should be positive");
        
        RuleFor(x => x.TypeId)
            .IsInEnum()
                .WithMessage("TypeId should be a valid enum value");
        
        RuleFor(x => x.StartDate)
            .NotEmpty()
                .WithMessage("StartDate should not be empty");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .When(x => x.EndDate.HasValue)
                .WithMessage("EndDate should be greater than StartDate");
    }
}