using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Expense;

public record ExpenseRequest
(
    DateTime date,
    string category,
    int? taxId,
    long? partSetId,
    ExpenseTypeEnum expenseTypeId,
    decimal sum
);
