using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseItem
(
    long id,
    DateTime date,
    string category,
    string? tax,
    int? taxId,
    long? partSetId,
    string expenseType,
    int expenceTypeId,
    decimal sum
);
