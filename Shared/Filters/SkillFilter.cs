namespace Shared.Filters;

public record SkillFilter
(
    IEnumerable<int> WorkerIds,
    IEnumerable<int> SpecializationIds,
    string? SortBy,
    bool IsDescending
);
