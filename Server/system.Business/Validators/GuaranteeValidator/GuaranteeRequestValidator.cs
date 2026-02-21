using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Guarantee;

namespace CRMSystem.Business.Validators.GuaranteeValidator;

public class GuaranteeRequestValidator : AbstractValidator<GuaranteeRequest>
{
    public GuaranteeRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
                .WithMessage("OrderId should be positive");

        RuleFor(x => x.DateStart)
            .NotEmpty()
                .WithMessage("DateStart should not be empty");

        RuleFor(x => x.DateEnd)
            .NotEmpty()
                .WithMessage("DateEnd should not be empty")
            .GreaterThan(x => x.DateStart)
                .WithMessage("DateEnd should be greater than DateStart");

        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.Terms)
            .NotEmpty()
                .WithMessage("Terms should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
                .WithMessage($"Terms should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
    }
}