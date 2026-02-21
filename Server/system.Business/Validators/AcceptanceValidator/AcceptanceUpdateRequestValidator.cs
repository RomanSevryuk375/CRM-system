using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Acceptance;

namespace CRMSystem.Business.Validators.AcceptanceValidator;

public class AcceptanceUpdateRequestValidator : AbstractValidator<AcceptanceUpdateRequest>
{
    public AcceptanceUpdateRequestValidator()
    {
        RuleFor(x => x.Mileage)
            .GreaterThan(0)
            .When(x => x.Mileage.HasValue)
                .WithMessage("Mileage should be positive");
        
        RuleFor(x => x.FuelLevel)
            .GreaterThan(0)
            .When(x => x.FuelLevel.HasValue)
                .WithMessage("Fuel level should be positive");

        RuleFor(x => x.ExternalDefects)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.ExternalDefects is not null)
                .WithMessage($"External Defects should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.InternalDefects)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.InternalDefects is not null)
                .WithMessage($"Internal Defects should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
        
        RuleFor(x => x)
            .Must(x =>
                x.ExternalDefects is not null || 
                x.InternalDefects is not null ||
                x.FuelLevel.HasValue || 
                x.Mileage.HasValue ||
                x.ClientSign.HasValue ||
                x.WorkerSign.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}