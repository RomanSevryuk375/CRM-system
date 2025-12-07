namespace CRMSystem.DataAccess.Entites;

public class OrderEntity
{
    public long Id { get; set; }
    public int StatusId { get; set; }
    public long CarId { get; set; }
    public DateTime Date { get; set; }
    public string Priority { get; set; } = string.Empty;

    public CarEntity? Car { get; set; }
    public OrderStatusEntity? Status { get; set; }
    public GuaranteesEntity? Guarantees { get; set; }
    public ICollection<AttachmentEntity> Attachments { get; set; } = new List<AttachmentEntity>();
    public ICollection<AcceptanceEntity> Acceptance { get; set; } = new List<AcceptanceEntity>();
    public ICollection<WorkInOrderEntity> Works { get; set; } = new HashSet<WorkInOrderEntity>();
    public ICollection<WorkProposalEntity> WorkProposals { get; set; }  = new HashSet<WorkProposalEntity>();
    public ICollection<BillEntity> Bills { get; set; } = new List<BillEntity>();
    public ICollection<PartSetEntity> UsedParts { get; set; } = new HashSet<PartSetEntity>();
}
