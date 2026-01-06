namespace CRMSystem.Core.ProjectionModels.Expense;

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
