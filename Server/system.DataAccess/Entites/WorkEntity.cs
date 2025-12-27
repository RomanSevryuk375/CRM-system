namespace CRMSystem.DataAccess.Entites;

public class WorkEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal StandardTime { get; set; }

    public ICollection<WorkInOrderEntity> WorksInOrder { get; set; } = new HashSet<WorkInOrderEntity>();
    public ICollection<WorkProposalEntity> WorkProposals { get; set; } = new HashSet<WorkProposalEntity>(); 
}
