using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Acceptance;

namespace CRMSystem.Business.Validators.AcceptanceValidator;

public class AcceptanceRequestValidator : AbstractValidator<AcceptanceRequest>
{
    public AcceptanceRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
                .WithMessage("OrderId should be positive");

        RuleFor(x => x.WorkerId)
            .GreaterThan(0)
                .WithMessage("WorkerId should be positive");
        
        RuleFor(x => x.CreatedAt)
            .NotEmpty()
                .WithMessage("CreatedAt should not be empty")
            .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"CreatedAt should be less than or equal to {DateTime.Now}");
        
        RuleFor(x => x.Mileage)
            .GreaterThan(0)
                .WithMessage("Mileage should be positive");
        
        RuleFor(x => x.FuelLevel)
            .GreaterThan(0)
                .WithMessage("Fuel level should be positive");

        RuleFor(x => x.ExternalDefects)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.ExternalDefects is not null)
                .WithMessage($"External Defects should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.InternalDefects)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.InternalDefects is not null)
                .WithMessage($"Internal Defects should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
    }
}