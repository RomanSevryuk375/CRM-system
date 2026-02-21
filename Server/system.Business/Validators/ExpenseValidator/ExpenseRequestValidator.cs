using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Expense;

namespace CRMSystem.Business.Validators.ExpenseValidator;

public class ExpenseRequestValidator : AbstractValidator<ExpenseRequest>
{
    public ExpenseRequestValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty().NotNull()
                .WithMessage("Date should not be empty")
            .LessThanOrEqualTo(DateTime.Now)
                .WithMessage($"Date should be less than or equal to {DateTime.Now}");

        RuleFor(x => x.Category)
            .NotEmpty()
                .WithMessage("Category should not be empty")
            .MaximumLength(ValidationConstants.MAX_CATEGORY_LENGTH)
                .WithMessage($"Category should be shorter than {ValidationConstants.MAX_CATEGORY_LENGTH}");

        RuleFor(x => x.TaxId)
            .GreaterThan(0)
            .When(x => x.TaxId.HasValue)
                .WithMessage("TaxId should be positive");

        RuleFor(x => x.PartSetId)
            .GreaterThan(0)
            .When(x => x.PartSetId.HasValue)
                .WithMessage("PartSetId should be positive");

        RuleFor(x => x.ExpenseTypeId)
            .IsInEnum()
                .WithMessage("ExpenseTypeId should be a valid enum value");

        RuleFor(x => x.Sum)
            .GreaterThan(0)
                .WithMessage("Sum should be positive");
    }
}