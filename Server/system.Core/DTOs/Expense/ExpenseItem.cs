using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseItem
(
    long Id,
    DateTime Date,
    string Category,
    string? Tax,
    int? TaxId,
    long? PartSetId,
    string ExpenseType,
    int ExpenceTypeId,
    decimal Sum
);
