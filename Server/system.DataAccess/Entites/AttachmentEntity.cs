namespace CRMSystem.DataAccess.Entites;

public class AttachmentEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public int WorkerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; } = string.Empty;

    public ICollection<AttachmentImgEntity> AttachmentImg { get; set; } = new List<AttachmentImgEntity>();
    public OrderEntity? Order { get; set; }
    public WorkerEntity? Worker { get; set; }
}
