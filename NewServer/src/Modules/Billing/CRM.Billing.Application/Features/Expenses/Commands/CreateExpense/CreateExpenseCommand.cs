using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Expenses.Commands.CreateExpense;

public sealed record CreateExpenseCommand(
    Guid ExpenseId,
    DateOnly Date,
    string Category,
    string? Description,
    int TypeId,
    decimal Amount,
    Guid? TaxId,
    Guid? ReferenceId) : ICommand;