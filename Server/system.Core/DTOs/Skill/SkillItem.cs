namespace CRMSystem.Core.DTOs.Skill;

public record SkillItem
(
    int Id,
    string worker,
    int WorkerId,
    string Specialization,
    int SpecializationId
);
