namespace CRMSystem.Core.ProjectionModels.Skill;

public record SkillItem
{
    public int Id { get; init; }
    public string Worker { get; init; } = string.Empty;
    public int WorkerId { get; init; }
    public string Specialization { get; init; } = string.Empty;
    public int SpecializationId { get; init; }
};
