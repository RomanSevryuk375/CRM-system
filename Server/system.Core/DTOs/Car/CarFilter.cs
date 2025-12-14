namespace CRMSystem.Core.DTOs.Car;

public record CarFilter
(
    IEnumerable<long> ownerIds,
    string? SortBy,
    int Page,
    int Limit,
    bool isDescending
);

