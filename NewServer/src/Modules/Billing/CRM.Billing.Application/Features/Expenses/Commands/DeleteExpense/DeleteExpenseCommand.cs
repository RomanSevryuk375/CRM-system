using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Expenses.Commands.DeleteExpense;

public sealed record DeleteExpenseCommand(Guid ExpenseId) : ICommand;