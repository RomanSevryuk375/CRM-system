namespace CRMSystem.Core.ProjectionModels.Client;

public record ClientItem
{
    public long Id { get; init; }
    public long UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
};
