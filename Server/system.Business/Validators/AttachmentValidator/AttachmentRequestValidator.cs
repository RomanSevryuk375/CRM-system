using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Attachment;

namespace CRMSystem.Business.Validators.AttachmentValidator;

public class AttachmentRequestValidator : AbstractValidator<AttachmentRequest>
{
    public AttachmentRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .GreaterThan(0)
                .WithMessage("OrderId should be positive");
        
        RuleFor(x => x.WorkerId)
            .GreaterThan(0)
                .WithMessage("WorkerId should be positive");
        
        RuleFor(x => x.CreatedAt) 
            .NotEmpty()
                .WithMessage("CreateAt should not be empty")
            .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"CreateAt should be less than or equal to {DateTime.Now}");
        
        RuleFor(x => x.Description)
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Description is not null)
                .WithMessage($"Description should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
    }
}