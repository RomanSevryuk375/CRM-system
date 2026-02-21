using FluentValidation;
using Shared.Contracts.Schedule;

namespace CRMSystem.Business.Validators.ScheduleValidator;

public class ScheduleRequestValidator : AbstractValidator<ScheduleRequest>
{
    public ScheduleRequestValidator()
    {
        RuleFor(x => x.WorkerId)
            .GreaterThan(0)
                .WithMessage("WorkerId should be positive");

        RuleFor(x => x.ShiftId)
            .GreaterThan(0)
                .WithMessage("ShiftId should be positive");

        RuleFor(x => x.DateTime)
            .NotEmpty()
                .WithMessage("Date Time should not be empty");
    }
}