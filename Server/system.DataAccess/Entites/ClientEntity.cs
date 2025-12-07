namespace CRMSystem.DataAccess.Entites;

public class ClientEntity
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public UserEntity? User { get; set; }
    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
    public ICollection<NotificationEntity> Notiffications { get; set; } = new List<NotificationEntity>();
}
