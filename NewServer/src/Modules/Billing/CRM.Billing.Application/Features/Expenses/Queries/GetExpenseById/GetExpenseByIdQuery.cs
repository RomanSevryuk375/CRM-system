// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Expenses.Queries.GetExpenseById;

public sealed record GetExpenseByIdQuery(Guid ExpenseId) : IQuery<ExpenseDetailsDto?>;

public sealed record ExpenseDetailsDto(
    Guid Id,
    DateOnly Date,
    string Category,
    string? Description,
    int TypeId,
    decimal Amount,
    Guid? TaxId,
    Guid? ReferenceId,
    DateTimeOffset CreatedAt);