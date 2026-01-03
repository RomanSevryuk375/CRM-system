using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseFilter
(
    IEnumerable<int?> TaxIds,
    IEnumerable<long?> PartSetIds,
    IEnumerable<int> ExpenseTypeIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
