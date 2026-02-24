using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Supplier;

namespace CRMSystem.Business.Validators.SupplierValidator;

public class SupplierUpdateRequestValidator : AbstractValidator<SupplierUpdateRequest>
{
    public SupplierUpdateRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .When(x => x.Name is not null)
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
            .When(x => x.Name is not null)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Contacts)
            .NotEmpty()
            .When(x => x.Contacts is not null)
                .WithMessage("Contacts should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
            .When(x => x.Contacts is not null)
                .WithMessage($"Contacts should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");

        RuleFor(x => x)
            .Must(x => 
                x.Name is not null 
                || x.Contacts is not null)
            .WithMessage("At least one field should be provided for update");
    }
}