namespace CRMSystem.DataAccess.Entites;

public class StatusEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ICollection<WorkEntity> Works { get; set; } = new List<WorkEntity>();

    public ICollection<BillEntity> Bills { get; set; } = new List<BillEntity>();

    public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();

    public ICollection<WorkProposalEntity> WorkProposals { get; set; } = new HashSet<WorkProposalEntity>();
}
