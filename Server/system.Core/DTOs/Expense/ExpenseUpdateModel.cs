using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseUpdateModel
(
    DateTime? date,
    string? category,
    ExpenseTypeEnum? expenseTypeId,
    decimal? sum
);
