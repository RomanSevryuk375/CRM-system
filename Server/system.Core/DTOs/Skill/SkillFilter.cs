namespace CRMSystem.Core.DTOs.Skill;

public record SkillFilter
(
    IEnumerable<int> workerIds,
    IEnumerable<int> specializationIds,
    string? SortBy,
    bool isDescending
);
