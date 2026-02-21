using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Guarantee;

namespace CRMSystem.Business.Validators.GuaranteeValidator;

public class GuaranteeUpdateRequestValidator : AbstractValidator<GuaranteeUpdateRequest>
{
    public GuaranteeUpdateRequestValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x.Terms)
            .NotEmpty()
            .When(x => x.Terms is not null)
                .WithMessage("Terms should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Terms is not null)
                .WithMessage($"Terms should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x)
            .Must(x => 
                x.Description is not null 
                || x.Terms is not null)
                .WithMessage("At least one field should be provided for update");
    }
}