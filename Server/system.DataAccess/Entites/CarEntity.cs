namespace CRMSystem.DataAccess.Entites;

public class CarEntity
{
    public long Id { get; set; }
    public long OwnerId { get; set; }
    public int StatusId { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int YearOfManufacture { get; set; }
    public string VinNumber { get; set; } = string.Empty;
    public string StateNumber { get; set; } = string.Empty;
    public int Mileage { get; set; }

    public ClientEntity? Client { get; set; }
    public CarStatusEntity? Status { get; set; }
    public ICollection<OrderEntity> Orders { get; set; } = new HashSet<OrderEntity>();
    public ICollection<NotificationEntity> Notifications { get; set; } = new HashSet<NotificationEntity>();
}
