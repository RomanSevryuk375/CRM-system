using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Tax;

namespace CRMSystem.Business.Validators.TaxValidator;

public class TaxRequestValidator : AbstractValidator<TaxRequest>
{
    public TaxRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x.Rate)
            .GreaterThan(0)
                .WithMessage("Rate should be positive");

        RuleFor(x => x.TypeId)
            .IsInEnum()
                .WithMessage("TypeId should be a valid enum value");
    }
}