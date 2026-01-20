using Shared.Enums;

namespace Shared.Contracts.Expense;

public record ExpenseRequest
(
    DateTime Date,
    string Category,
    int? TaxId,
    long? PartSetId,
    ExpenseTypeEnum ExpenseTypeId,
    decimal Sum
);
