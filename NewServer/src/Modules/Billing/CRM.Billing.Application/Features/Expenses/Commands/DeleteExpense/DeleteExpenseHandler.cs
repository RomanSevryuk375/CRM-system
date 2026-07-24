using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Results;
using MediatR;

namespace CRM.Billing.Application.Features.Expenses.Commands.DeleteExpense;

internal sealed class DeleteExpenseHandler(IExpenseRepository expenseRepository)
    : IRequestHandler<DeleteExpenseCommand, Result>
{
    public async Task<Result> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        ExpenseId expenseId = new(request.ExpenseId);
        Expense? expense = await expenseRepository.GetByIdAsync(expenseId, cancellationToken);
        if (expense is null)
        {
            return Result.Success();
        }

        expenseRepository.Delete(expense);

        return Result.Success();
    }
}