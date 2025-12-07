namespace CRMSystem.DataAccess.Entites;

public class WorkProposalStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<WorkProposalEntity> Proposals { get; set; } = new List<WorkProposalEntity>();
}
