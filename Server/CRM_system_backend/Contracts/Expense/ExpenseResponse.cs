namespace CRM_system_backend.Contracts.Expense;

public record ExpenseResponse
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
