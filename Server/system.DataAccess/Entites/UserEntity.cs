namespace CRMSystem.DataAccess.Entites;

public class UserEntity
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Login { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public ClientEntity? Client { get; set; }

    public WorkerEntiеy? Worker { get; set; }

    public RoleEntity? Role { get; set; }
}
