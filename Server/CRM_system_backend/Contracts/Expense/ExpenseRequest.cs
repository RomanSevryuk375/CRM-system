using CRMSystem.Core.Enums;

namespace CRM_system_backend.Contracts.Expense;

public record ExpenseRequest
(
    DateTime Date,
    string Category,
    int? TaxId,
    long? PartSetId,
    ExpenseTypeEnum ExpenseTypeId,
    decimal Sum
);
