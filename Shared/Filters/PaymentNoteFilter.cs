namespace Shared.Filters;

public record PaymentNoteFilter
(
    IEnumerable<long?> BillIds,
    IEnumerable<int>? MethodIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
