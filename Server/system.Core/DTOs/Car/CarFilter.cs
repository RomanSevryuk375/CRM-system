namespace CRMSystem.Core.DTOs.Car;

public record CarFilter
(
    IEnumerable<long> OwnerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);

