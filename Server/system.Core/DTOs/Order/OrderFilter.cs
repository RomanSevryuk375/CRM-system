using CRMSystem.Core.Enums;

namespace CRMSystem.Core.DTOs.Order;

public record OrderFilter
(
    IEnumerable<long>? OrderIds,
    IEnumerable<int>? StatusIds,
    IEnumerable<int>? PriorityIds,
    IEnumerable<long>? CarIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
