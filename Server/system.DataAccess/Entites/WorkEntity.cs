namespace CRMSystem.DataAccess.Entites;

public class WorkEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int JobId { get; set; }

    public int WorkerId { get; set; }

    public decimal TimeSpent { get; set; }

    public int StatusId { get; set; }

    public WorkTypeEntity? WorkType { get; set; }

    public OrderEntity? Order { get; set; }

    public StatusEntity? Status { get; set; }

    public WorkerEntity? Worker { get; set; }

}
