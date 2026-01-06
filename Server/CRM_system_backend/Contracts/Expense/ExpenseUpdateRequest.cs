using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Expense;

public record ExpenseUpdateRequest
(
    DateTime? Date,
    string? Category,
    ExpenseTypeEnum? ExpenseTypeId,
    decimal? Sum
);
