using FluentValidation;
using Shared.Contracts.Schedule;

namespace CRMSystem.Business.Validators.ScheduleValidator;

public class ScheduleUpdateRequestValidator : AbstractValidator<ScheduleUpdateRequest>
{
    public ScheduleUpdateRequestValidator()
    {
        RuleFor(x => x.ShiftId)
            .GreaterThan(0)
            .When(x => x.ShiftId.HasValue)
                .WithMessage("ShiftId should be positive");

        RuleFor(x => x.DateTime)
            .NotEmpty()
            .When(x => x.DateTime.HasValue)
                .WithMessage("Date Time should not be empty");

        RuleFor(x => x)
            .Must(x => 
                x.ShiftId.HasValue 
                || x.DateTime.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}