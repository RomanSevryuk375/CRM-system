namespace CRMSystem.DataAccess.Entites;

public class ClientEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public UserEntity? User { get; set; }

    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
}
