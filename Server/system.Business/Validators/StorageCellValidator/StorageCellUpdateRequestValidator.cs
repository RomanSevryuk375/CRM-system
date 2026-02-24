using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.StorageCell;

namespace CRMSystem.Business.Validators.StorageCellValidator;

public class StorageCellUpdateRequestValidator : AbstractValidator<StorageCellUpdateRequest>
{
    public StorageCellUpdateRequestValidator()
    {
        RuleFor(x => x.Rack)
            .NotEmpty()
            .When(x => x.Rack is not null)
                .WithMessage("Rack should not be empty")
            .MaximumLength(ValidationConstants.MAX_STORAGE_ITEM_LENGTH)
            .When(x => x.Rack is not null)
                .WithMessage($"Rack should be shorter than {ValidationConstants.MAX_STORAGE_ITEM_LENGTH}");

        RuleFor(x => x.Shelf)
            .NotEmpty()
            .When(x => x.Shelf is not null)
                .WithMessage("Shelf should not be empty")
            .MaximumLength(ValidationConstants.MAX_STORAGE_ITEM_LENGTH)
            .When(x => x.Shelf is not null)
                .WithMessage($"Shelf should be shorter than {ValidationConstants.MAX_STORAGE_ITEM_LENGTH}");

        RuleFor(x => x)
            .Must(x => 
                x.Rack is not null 
                || x.Shelf is not null)
                .WithMessage("At least one field should be provided for update");
    }
}