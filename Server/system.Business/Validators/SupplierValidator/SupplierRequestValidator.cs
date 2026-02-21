using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Supplier;

namespace CRMSystem.Business.Validators.SupplierValidator;

public class SupplierRequestValidator : AbstractValidator<SupplierRequest>
{
    public SupplierRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .WithMessage("Name should not be empty")
            .MaximumLength(ValidationConstants.MAX_NAME_LENGTH)
                .WithMessage($"Name should be shorter than {ValidationConstants.MAX_NAME_LENGTH}");

        RuleFor(x => x.Contacts)
            .NotEmpty()
                .WithMessage("Contacts should not be empty")
            .MaximumLength(ValidationConstants.MAX_DESCRIPTION_LENGTH)
                .WithMessage($"Contacts should be shorter than {ValidationConstants.MAX_DESCRIPTION_LENGTH}");
    }
}