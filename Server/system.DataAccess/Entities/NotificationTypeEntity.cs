namespace CRMSystem.DataAccess.Entities;

public class NotificationTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<NotificationEntity> Notifications { get; set; } = new HashSet<NotificationEntity>();
}
