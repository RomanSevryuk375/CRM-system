using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.AcceptanceImg;

namespace CRMSystem.Business.Validators.AcceptanceImgValidator;

public class AcceptanceImgUpdateRequestValidator : AbstractValidator<AcceptanceImgUpdateRequest>
{
    public AcceptanceImgUpdateRequestValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x)
            .Must(x => 
                x.FilePath is not null 
                || x.Description is not null)
            .WithMessage("At least one field should be provided for update");
    }
}