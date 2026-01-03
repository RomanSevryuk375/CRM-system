using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseUpdateModel
(
    DateTime? Date,
    string? category,
    ExpenseTypeEnum? ExpenseTypeId,
    decimal? Sum
);
