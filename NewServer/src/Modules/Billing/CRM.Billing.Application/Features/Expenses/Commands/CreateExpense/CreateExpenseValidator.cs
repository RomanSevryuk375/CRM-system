// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Expenses.Commands.CreateExpense;

public sealed class CreateExpenseValidator
    : AbstractValidator<CreateExpenseCommand>
{
    public CreateExpenseValidator()
    {
        RuleFor(x => x.ExpenseId)
            .NotEmpty();

        RuleFor(x => x.Category)
            .NotEmpty();

        RuleFor(x => x.TypeId)
            .IsInEnum();

        RuleFor(x => x.Amount)
            .GreaterThan(0);
    }
}