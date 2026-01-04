namespace CRMSystem.Core.DTOs.Skill;

public record SkillFilter
(
    IEnumerable<int> WorkerIds,
    IEnumerable<int> specializationIds,
    string? SortBy,
    bool IsDescending
);
