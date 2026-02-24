using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.AttachmentImg;

namespace CRMSystem.Business.Validators.AttachmentImgValidator;

public class AttachmentImgUpdateRequestValidator : AbstractValidator<AttachmentImgUpdateRequest>
{
    public AttachmentImgUpdateRequestValidator()
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