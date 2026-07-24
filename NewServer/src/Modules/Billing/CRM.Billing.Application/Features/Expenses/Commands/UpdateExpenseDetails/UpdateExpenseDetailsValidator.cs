// Ignore Spelling: Validator

using FluentValidation;

namespace CRM.Billing.Application.Features.Expenses.Commands.UpdateExpenseDetails;

public sealed class UpdateExpenseDetailsValidator
    : AbstractValidator<UpdateExpenseDetailsCommand>
{
    public UpdateExpenseDetailsValidator()
    {
        RuleFor(x => x.ExpenseId)
            .NotEmpty();

        RuleFor(x => x.Category)
            .NotEmpty();
    }
}