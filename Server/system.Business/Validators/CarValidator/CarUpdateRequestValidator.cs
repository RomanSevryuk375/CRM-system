using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Car;

namespace CRMSystem.Business.Validators.CarValidator;

public class CarUpdateRequestValidator : AbstractValidator<CarUpdateRequest>
{
    public CarUpdateRequestValidator()
    {
        RuleFor(x => x.StatusId)
            .IsInEnum()
            .When(x => x.StatusId.HasValue)
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.Brand)
            .NotEmpty()
            .When(x => x.Brand is not null )
                .WithMessage("Brand should not be empty")
            .MaximumLength(ValidationConstants.MAX_BRAND_LENGTH)
            .When(x => x.Brand != null)
                .WithMessage($"Brand should be shorter than {ValidationConstants.MAX_BRAND_LENGTH}");

        RuleFor(x => x.Model)
            .NotEmpty()
            .When(x => x.Model is not null )
                .WithMessage("Model should not be empty")
            .MaximumLength(ValidationConstants.MAX_MODEL_LENGTH)
            .When(x => x.Model != null)
                .WithMessage($"Model should be shorter than {ValidationConstants.MAX_MODEL_LENGTH}");

        RuleFor(x => x.YearOfManufacture)
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
            .When(x => x.YearOfManufacture.HasValue)
                .WithMessage($"Year of manufacture should be between 1900 and {DateTime.Now.Year + 1}");

        RuleFor(x => x.Mileage)
            .Must(x => x >= 0)
            .When(x => x.Mileage.HasValue)
                .WithMessage("Mileage should be positive or zero");

        RuleFor(x => x)
            .Must(x => 
                x.StatusId.HasValue 
                || x.Brand is not null 
                || x.Model is not null 
                || x.YearOfManufacture.HasValue 
                || x.Mileage.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}