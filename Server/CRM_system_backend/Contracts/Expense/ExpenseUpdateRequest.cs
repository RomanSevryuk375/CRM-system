using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Expense;

public record ExpenseUpdateRequest
(
    DateTime? date,
    string? category,
    ExpenseTypeEnum? expenseTypeId,
    decimal? sum
);
