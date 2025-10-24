namespace CRMSystem.DataAccess.Entites;

public class WorkTypeEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal StandardTime { get; set; }

    public ICollection<WorkProposalEntity> WorkProposals { get; set; } = new List<WorkProposalEntity>();

    public ICollection<WorkEntity> Works { get; set; } = new HashSet<WorkEntity>();
}
