namespace CRMSystem.DataAccess.Entites;

public class SkillEntity
{
    public int  Id { get; set; }
    public int WorkerId { get; set; }
    public int SpecializationId { get; set; }

    public WorkerEntity? Worker { get; set; }
    public SpecializationEntity? Specialization { get; set; } 

}
