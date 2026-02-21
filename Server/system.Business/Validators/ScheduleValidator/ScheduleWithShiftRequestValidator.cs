using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Schedule;

namespace CRMSystem.Business.Validators.ScheduleValidator;

public class ScheduleWithShiftRequestValidator : AbstractValidator<ScheduleWithShiftRequest>
{
    public ScheduleWithShiftRequestValidator()
    {
        RuleFor(x => x.WorkerId)
            .GreaterThan(0)
                .WithMessage("WorkerId should be positive");

        RuleFor(x => x.DateTime)
            .NotEmpty()
                .WithMessage("Date Time should not be empty");
        
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Shift Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
                .WithMessage($"Shift Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x.StartAt)
            .NotEmpty()
                .WithMessage("Start Time should not be empty");

        RuleFor(x => x.EndAt)
            .NotEmpty()
                .WithMessage("End Time should not be empty")
            .GreaterThan(x => x.StartAt)
                .WithMessage("End Time should be greater than Start Time");
    }
}