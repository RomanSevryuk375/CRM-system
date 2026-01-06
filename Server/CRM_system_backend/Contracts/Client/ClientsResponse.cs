namespace CRM_system_backend.Contracts.Client;

public record ClientsResponse
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
};
