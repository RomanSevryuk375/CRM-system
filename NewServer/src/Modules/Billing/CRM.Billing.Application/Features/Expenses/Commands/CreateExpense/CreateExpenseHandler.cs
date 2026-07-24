using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.Interfaces;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.CQRS;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Application.Features.Expenses.Commands.CreateExpense;

public sealed class CreateExpenseHandler(
    IExpenseRepository expenseRepository,
    ITaxRepository taxRepository,
    TimeProvider timeProvider)
    : ICommandHandler<CreateExpenseCommand>
{
    public async Task<Result> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        TaxId? taxId = null;
        if (request.TaxId.HasValue)
        {
            taxId = new(request.TaxId.Value);
            if (!await taxRepository.ExistsAsync(taxId.Value, cancellationToken))
            {
                return Result.Failure(Error.NotFound<Tax>(
                    "The specified tax was not found."));
            }
        }

        ExpenseId expenseId = new(request.ExpenseId);
        ExpenseType type = (ExpenseType)request.TypeId;
        DateOnly today = DateOnly.FromDateTime(timeProvider.GetUtcNow().Date);

        Result<Expense> result = Expense.Create(
            expenseId,
            request.Date,
            request.Category,
            request.Description,
            type,
            request.Amount,
            today,
            taxId,
            request.ReferenceId);
        if (result.IsFailure)
        {
            return result;
        }

        await expenseRepository.AddAsync(result.Value, cancellationToken);

        return Result.Success();
    }
}