namespace CRMSystem.DataAccess.Entities;

public class NotificationStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<NotificationEntity> Notifications { get; set; } = new HashSet<NotificationEntity>();
}
