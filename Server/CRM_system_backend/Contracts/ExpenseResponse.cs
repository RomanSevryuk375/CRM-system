namespace CRM_system_backend.Contracts;

public record ExpenseResponse
(
    int Id,
    DateTime Date,
    string Category,
    int? TaxId,
    int? UsedPartId,
    string ExpenseType,
    decimal Sum
);
