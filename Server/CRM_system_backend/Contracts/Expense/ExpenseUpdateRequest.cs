using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseUpdateRequest
(
    DateTime? Date,
    string? Category,
    ExpenseTypeEnum? ExpenseTypeId,
    decimal? Sum
);
