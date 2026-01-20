using Shared.Enums;

namespace Shared.Contracts.Expense;

public record ExpenseUpdateRequest
(
    DateTime? Date,
    string? Category,
    ExpenseTypeEnum? ExpenseTypeId,
    decimal? Sum
);
