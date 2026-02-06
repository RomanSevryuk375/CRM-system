namespace Shared.Filters;

public record NotificationFilter
(
    IEnumerable<long>? ClientIds,
    IEnumerable<long>? CarIds,
    IEnumerable<int>? TypeIds,
    IEnumerable<int>? StatusIds,
    string? SortBy,
    int Page,
    int Limit,
    bool IsDescending
);
