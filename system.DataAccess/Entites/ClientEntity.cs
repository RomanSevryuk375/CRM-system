namespace system.DataAccess.Entites;

public class ClientEntity
{
    public int client_id { get; set; }

    public int client_user_id { get; set; }

    public string client_name { get; set; } = string.Empty;

    public string client_surname { get; set; } = string.Empty;

    public string client_phone_number { get; set; } = string.Empty;

    public string client_email { get; set; } = string.Empty;

}
