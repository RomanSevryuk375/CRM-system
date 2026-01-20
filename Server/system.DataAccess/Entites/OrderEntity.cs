namespace CRMSystem.DataAccess.Entites;

public class OrderEntity
{
    public long Id { get; set; }
    public int StatusId { get; set; }
    public long CarId { get; set; }
    public DateOnly Date { get; set; }
    public int PriorityId { get; set; }

    public CarEntity? Car { get; set; }
    public OrderStatusEntity? Status { get; set; }
    public OrderPriorityEntity? OrderPriority { get; set; }
    public ICollection<GuaranteeEntity> Guarantees { get; set; } = new HashSet<GuaranteeEntity>();
    public ICollection<AttachmentEntity> Attachments { get; set; } = new HashSet<AttachmentEntity>();
    public ICollection<AcceptanceEntity> Acceptances { get; set; } = new HashSet<AcceptanceEntity>();
    public ICollection<WorkInOrderEntity> WorksInOrder { get; set; } = new HashSet<WorkInOrderEntity>();
    public ICollection<WorkProposalEntity> WorkProposals { get; set; }  = new HashSet<WorkProposalEntity>();
    public ICollection<BillEntity> Bills { get; set; } = new HashSet<BillEntity>();
    public ICollection<PartSetEntity> PartSets { get; set; } = new HashSet<PartSetEntity>();
}
