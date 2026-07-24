using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Expenses.Commands.UpdateExpenseDetails;

public sealed record UpdateExpenseDetailsCommand(
    Guid ExpenseId,
    DateOnly Date,
    string Category,
    string? Description) : ICommand;