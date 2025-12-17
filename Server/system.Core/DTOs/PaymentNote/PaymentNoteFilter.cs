using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.PaymentNote;

public record PaymentNoteFilter
(
    IEnumerable<long?> billIds,
    IEnumerable<PaymentMethodEnum> methodIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);
