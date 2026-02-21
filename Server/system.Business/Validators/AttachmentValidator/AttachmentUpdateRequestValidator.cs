using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Attachment;

namespace CRMSystem.Business.Validators.AttachmentValidator;

public class AttachmentUpdateRequestValidator : AbstractValidator<AttachmentUpdateRequest>
{
    public AttachmentUpdateRequestValidator()
    {
        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x)
            .Must(x => x.Description is not null)
                .WithMessage("Description should be provided for update");
    }
}