using CRMSystem.Core.Validation;
using FluentValidation;
using Shared.Contracts.Expense;

namespace CRMSystem.Business.Validators.ExpenseValidator;

public class ExpenseUpdateRequestValidator : AbstractValidator<ExpenseUpdateRequest>
{
    public ExpenseUpdateRequestValidator()
    {
        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Now)
            .When(x => x.Date.HasValue)
                .WithMessage($"Date should be less than or equal to {DateTime.Now}");

        RuleFor(x => x.Category)
            .NotEmpty()
            .When(x => x.Category is not null)
                .WithMessage("Category should not be empty")
            .MaximumLength(ValidationConstants.MAX_CATEGORY_LENGTH)
            .When(x => x.Category is not null)
                .WithMessage($"Category should be shorter than {ValidationConstants.MAX_CATEGORY_LENGTH}");

        RuleFor(x => x.ExpenseTypeId)
            .IsInEnum()
            .When(x => x.ExpenseTypeId.HasValue)
                .WithMessage("ExpenseTypeId should be a valid enum value");

        RuleFor(x => x.Sum)
            .GreaterThan(0)
            .When(x => x.Sum.HasValue)
                .WithMessage("Sum should be positive");

        RuleFor(x => x)
            .Must(x => 
                x.Date.HasValue
                || x.Category is not null 
                || x.ExpenseTypeId.HasValue
                || x.Sum.HasValue)
            .WithMessage("At least one field should be provided for update");
    }
}