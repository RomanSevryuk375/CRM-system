using CRMSystem.Core.Enums;

namespace CRMSystem.DataAccess.Entites;

public class WorkInOrderEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long JobId { get; set; }
    public int WorkerId { get; set; }
    public decimal TimeSpent { get; set; }
    public int StatusId { get; set; }

    public WorkEntity? Work { get; set; }
    public OrderEntity? Order { get; set; }
    public WorkProposalEntity? WorkProposal { get; set; }
    public WorkerEntity? Worker { get; set; }
    public WorkInOrderStatusEntity? WorkInOrderStatus { get; set; }

}
