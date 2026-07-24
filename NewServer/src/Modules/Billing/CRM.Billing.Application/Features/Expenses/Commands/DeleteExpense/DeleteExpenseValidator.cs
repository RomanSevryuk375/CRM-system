// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Expenses.Commands.DeleteExpense;

public sealed class DeleteExpenseValidator
    : AbstractValidator<DeleteExpenseCommand>
{
    public DeleteExpenseValidator()
    {
        RuleFor(x => x.ExpenseId)
            .NotEmpty();
    }
}
