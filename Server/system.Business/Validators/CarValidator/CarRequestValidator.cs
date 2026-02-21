using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Car;
using Shared.Enums;

namespace CRMSystem.Business.Validators.CarValidator;

public class CarRequestValidator : AbstractValidator<CarRequest>
{
    public CarRequestValidator()
    {
        RuleFor(x => x.OwnerId)
            .NotEmpty().NotNull()
                .WithMessage("OwnerId should not be empty")
            .GreaterThan(0)
                .WithMessage("OwnerId should be positive");

        RuleFor(x => x.StatusId)
            .Must(x => Enum.IsDefined(typeof(CarStatusEnum), x))
                .WithMessage("StatusId should be a valid enum value");

        RuleFor(x => x.Brand)
            .NotEmpty()
                .WithMessage("Brand should not be empty")
            .MaximumLength(ValidationConstants.MAX_BRAND_LENGTH)
                .WithMessage($"Brand should be shorter than {ValidationConstants.MAX_BRAND_LENGTH}");

        RuleFor(x => x.Model)
            .NotEmpty()
                .WithMessage("Model should not be empty")
            .MaximumLength(ValidationConstants.MAX_MODEL_LENGTH)
                .WithMessage($"Model should be shorter than {ValidationConstants.MAX_MODEL_LENGTH}");

        RuleFor(x => x.YearOfManufacture)
            .NotEmpty()
                .WithMessage("Year of manufacture should not be empty")
            .InclusiveBetween(1900, DateTime.Now.Year + 1)
                .WithMessage($"Year of manufacture should be between 1900 and {DateTime.Now.Year + 1}");

        RuleFor(x => x.VinNumber)
            .NotEmpty()
                .WithMessage("VIN number should not be empty")
            .Matches(@"^[A-HJ-NPR-Z0-9]{17}$")
                .WithMessage("VIN number should be a valid 17-character alphanumeric string (excluding I, O, Q)");

        RuleFor(x => x.StateNumber)
            .NotEmpty()
                .WithMessage("State number should not be empty")
            .Matches(@"^(\d{4}\s?[ABEIKMHOPCTX]{2}-[1-7]|[ABEIKMHOPCTX]{2}\s?\d{4}-[1-7]|(TA|TT|TY)\d{4}|E\d{3}[ABEIKMHOPCTX]{2}[1-7])$")
                .WithMessage("State number should be in valid format (e.g., 1234 AB-7)");

        RuleFor(x => x.Mileage)
            .NotNull()
                .WithMessage("Mileage should not be null")
            .GreaterThanOrEqualTo(0)
                .WithMessage("Mileage should be positive or zero");
    }
}