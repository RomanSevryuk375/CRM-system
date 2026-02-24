using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.StorageCell;

namespace CRMSystem.Business.Validators.StorageCellValidator;

public class StorageCellRequestValidator : AbstractValidator<StorageCellRequest>
{
    public StorageCellRequestValidator()
    {
        RuleFor(x => x.Rack)
            .NotEmpty()
                .WithMessage("Rack should not be empty")
            .MaximumLength(ValidationConstants.MAX_STORAGE_ITEM_LENGTH)
                .WithMessage($"Rack should be shorter than {ValidationConstants.MAX_STORAGE_ITEM_LENGTH}");

        RuleFor(x => x.Shelf)
            .NotEmpty()
                .WithMessage("Shelf should not be empty")
            .MaximumLength(ValidationConstants.MAX_STORAGE_ITEM_LENGTH)
                .WithMessage($"Shelf should be shorter than {ValidationConstants.MAX_STORAGE_ITEM_LENGTH}");
    }
}