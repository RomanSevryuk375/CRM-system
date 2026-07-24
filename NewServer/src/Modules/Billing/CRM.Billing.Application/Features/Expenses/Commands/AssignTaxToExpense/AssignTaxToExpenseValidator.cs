// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Expenses.Commands.AssignTaxToExpense;

public sealed class AssignTaxToExpenseValidator
    : AbstractValidator<AssignTaxToExpenseCommand>
{
    public AssignTaxToExpenseValidator()
    {
        RuleFor(x => x.ExpenseId)
            .NotEmpty();

        RuleFor(x => x.TaxId)
            .NotEmpty();
    }
}