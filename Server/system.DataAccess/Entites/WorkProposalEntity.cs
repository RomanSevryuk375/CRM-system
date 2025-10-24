namespace CRMSystem.DataAccess.Entites;

public class WorkProposalEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int WorkId { get; set; }

    public int ByWorker { get; set; }

    public int StatusId { get; set; }

    public int DecisionStatusId { get; set; }

    public DateTime Date { get; set; }

    public OrderEntity? Order { get; set; }

    public WorkTypeEntity? WorkType { get; set; }

    public WorkerEntiеy? Worker { get; set; }

    public StatusEntity? Status { get; set; } 

    public ICollection<ProposedPartEntity> ProposedParts { get; set; } = new List<ProposedPartEntity>();

}
