using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseItem
(
    long id,
    DateTime date,
    string category,
    string? tax,
    long? partSetId,
    string expenseType,
    decimal sum
);
