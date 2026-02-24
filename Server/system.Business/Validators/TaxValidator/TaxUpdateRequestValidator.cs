using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Tax;

namespace CRMSystem.Business.Validators.TaxValidator;

public class TaxUpdateRequestValidator : AbstractValidator<TaxUpdateRequest>
{
    public TaxUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_TYPE_NAME)
            .When(x => x.Name is not null)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_TYPE_NAME}");

        RuleFor(x => x.Rate)
            .GreaterThan(0)
            .When(x => x.Rate.HasValue)
                .WithMessage("Rate should be positive");

        RuleFor(x => x)
            .Must(x => 
                x.Name is not null 
                || x.Rate.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}