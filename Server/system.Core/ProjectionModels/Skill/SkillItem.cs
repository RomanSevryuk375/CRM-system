namespace CRMSystem.Core.ProjectionModels.Skill;

public record SkillItem
(
    int Id,
    string Worker,
    int WorkerId,
    string Specialization,
    int SpecializationId
);
