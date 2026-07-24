using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.Results;
using MediatR;

namespace CRM.Billing.Application.Features.Expenses.Commands.AssignTaxToExpense;

internal sealed class AssignTaxToExpenseHandler(
    IExpenseRepository expenseRepository,
    ITaxRepository taxRepository)
    : IRequestHandler<AssignTaxToExpenseCommand, Result>
{
    public async Task<Result> Handle(AssignTaxToExpenseCommand request, CancellationToken cancellationToken)
    {
        TaxId taxId = new(request.TaxId);
        if (!await taxRepository.ExistsAsync(taxId, cancellationToken))
        {
            return Result.Failure(Error.NotFound<Tax>(
                "The specified tax was not found."));
        }

        ExpenseId expenseId = new(request.ExpenseId);
        Expense? expense = await expenseRepository.GetByIdAsync(expenseId, cancellationToken);
        if (expense is null)
        {
            return Result.Failure(Error.NotFound<Expense>(
                "The specified expense was not found."));
        }

        Result result = expense.AssignTax(taxId);
        if (result.IsFailure)
        {
            return Result.Failure(result.Error);
        }

        return Result.Success();
    }
}