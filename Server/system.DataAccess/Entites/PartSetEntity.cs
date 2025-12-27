namespace CRMSystem.DataAccess.Entites;

public class PartSetEntity
{
    protected PartSetEntity() { }

    public PartSetEntity(long? orderId, long positionId, long? proposalId, decimal quantity, decimal soldPrice)
    {
        OrderId = orderId;
        PositionId = positionId;
        ProposalId = proposalId;
        Quantity = quantity;
        SoldPrice = soldPrice;
    }
    public long Id { get; set; }
    public long? OrderId { get; set; }
    public long PositionId { get; set; }
    public long? ProposalId { get; set; }
    public decimal Quantity { get; set; }
    public decimal SoldPrice { get; set; }

    public ICollection<ExpenseEntity> Expenses { get; set; } = new HashSet<ExpenseEntity>();
    public OrderEntity? Order { get; set; }
    public WorkProposalEntity? WorkProposal { get; set; }
    public PositionEntity? Position { get; set; }
}
