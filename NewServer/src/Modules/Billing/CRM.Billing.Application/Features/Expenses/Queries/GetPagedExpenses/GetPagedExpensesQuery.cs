// Ignore Spelling: Dto

using CRM.Shared.Abstractions.CQRS;

namespace CRM.Billing.Application.Features.Expenses.Queries.GetPagedExpenses;

public sealed record GetPagedExpensesQuery(
    int Limit,
    int Offset,
    DateOnly? DateFrom = null,
    DateOnly? DateTo = null,
    int? TypeId = null,
    Guid? TaxId = null,
    Guid? ReferenceId = null) : IQuery<IReadOnlyList<ExpenseListItemDto>>;

public sealed record ExpenseListItemDto(
    Guid Id,
    DateOnly Date,
    string Category,
    int TypeId,
    decimal Amount,
    Guid? ReferenceId);