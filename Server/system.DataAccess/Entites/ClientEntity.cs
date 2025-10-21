namespace CRMSystem.DataAccess.Entites;

public class ClientEntity
{
    public int ClientId { get; set; }

    public int ClientUserId { get; set; }

    public string ClientName { get; set; } = string.Empty;

    public string ClientSurname { get; set; } = string.Empty;

    public string ClientPhoneNumber { get; set; } = string.Empty;

    public string ClientEmail { get; set; } = string.Empty;

    public UserEntity? User { get; set; }
}
