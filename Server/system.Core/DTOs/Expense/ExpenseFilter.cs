using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Expense;

public record ExpenseFilter
(
    IEnumerable<int?> taxIds,
    IEnumerable<long?> partSetIds,
    IEnumerable<int> expenseTypeIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
