using CRMSystem.Core.Enums;

namespace CRMSystem.DataAccess.Entites;

public class NotificationEntity
{
    public long Id { get; set; }
    public long ClientId { get; set; }
    public long CarId { get; set; }
    public int TypeId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime SendAt { get; set; }
    public int StatusId { get; set; }

    public NotificationTypeEntity? NotificationType { get; set; }
    public ClientEntity? Client { get; set; }
    public CarEntity? Car { get; set; }
    public NotificationStatusEntity? Status { get; set; }
}
