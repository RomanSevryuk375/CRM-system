namespace CRMSystem.Core.DTOs.Skill;

public record SkillItem
(
    int id,
    string worker, 
    int workerId,
    string specialization,
    int specializationId
);
