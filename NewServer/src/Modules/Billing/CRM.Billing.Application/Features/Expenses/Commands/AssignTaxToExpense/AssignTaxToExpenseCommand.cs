using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Expenses.Commands.AssignTaxToExpense;

public sealed record AssignTaxToExpenseCommand(
    Guid ExpenseId,
    Guid TaxId) : ICommand;