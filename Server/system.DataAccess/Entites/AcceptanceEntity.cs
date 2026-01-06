// Ignore Spelling: Imgs

namespace CRMSystem.DataAccess.Entites;

public class AcceptanceEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public int WorkerId { get; set; }
    public DateTime CreateAt { get; set; }
    public int Mileage { get; set; }
    public int FuelLevel { get; set; }
    public string? ExternalDefects { get; set; } = string.Empty;
    public string? InternalDefects { get; set; } = string.Empty;
    public bool? ClientSign { get; set; }
    public bool? WorkerSign { get; set; }

    public WorkerEntity? Worker { get; set; }
    public OrderEntity? Order { get; set; }
    public ICollection<AcceptanceImgEntity> Imgs { get; set; } = new HashSet<AcceptanceImgEntity>();
}
