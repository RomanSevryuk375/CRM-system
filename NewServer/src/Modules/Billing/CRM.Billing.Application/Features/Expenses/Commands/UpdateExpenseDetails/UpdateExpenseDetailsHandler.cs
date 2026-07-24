using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.Expenses.Commands.UpdateExpenseDetails;

public sealed class UpdateExpenseDetailsHandler(
    IExpenseRepository expenseRepository,
    TimeProvider timeProvider)
    : ICommandHandler<UpdateExpenseDetailsCommand>
{
    public async Task<Result> Handle(UpdateExpenseDetailsCommand request, CancellationToken cancellationToken)
    {
        ExpenseId expenseId = new(request.ExpenseId);
        Expense? expense = await expenseRepository.GetByIdAsync(expenseId, cancellationToken);
        if (expense is null)
        {
            return Result.Failure(Error.NotFound<Expense>(
                "The specified expense was not found."));
        }

        DateOnly today = DateOnly.FromDateTime(timeProvider.GetUtcNow().Date);
        Result result = expense.UpdateDetails(
            request.Date,
            request.Category,
            request.Description,
            today);
        if (result.IsFailure)
        {
            return result;
        }

        return Result.Success();
    }
}