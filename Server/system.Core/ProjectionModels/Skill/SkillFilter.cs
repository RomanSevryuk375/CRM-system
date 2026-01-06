namespace CRMSystem.Core.ProjectionModels.Skill;

public record SkillFilter
(
    IEnumerable<int> WorkerIds,
    IEnumerable<int> SpecializationIds,
    string? SortBy,
    bool IsDescending
);
