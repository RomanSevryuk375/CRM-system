using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.PaymentNote;

public record PaymentNoteFilter
(
    IEnumerable<long?> BillIds,
    IEnumerable<int> MethodIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
