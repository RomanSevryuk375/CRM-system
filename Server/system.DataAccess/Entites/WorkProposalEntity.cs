namespace CRMSystem.DataAccess.Entites;

public class WorkProposalEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long JobId { get; set; }
    public int ByWorker { get; set; }
    public int StatusId { get; set; }
    public DateTime Date { get; set; }

    public OrderEntity? Order { get; set; }
    public WorkEntity? Work { get; set; }
    public WorkerEntity? Worker { get; set; }
    public WorkProposalStatusEntity? Status { get; set; } 
    public PartSetEntity? PartSet { get; set; }
}
