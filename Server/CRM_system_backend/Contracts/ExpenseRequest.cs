namespace CRM_system_backend.Contracts;

public record ExpenseRequest
(
    DateTime Date,
    string Category,
    int? TaxId,
    int? UsedPartId,
    string ExpenseType,
    decimal Sum
);
