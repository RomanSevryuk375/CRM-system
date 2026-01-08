namespace CRMSystem.Core.ProjectionModels.User;

public record UserItem
{
    public long Id { get; init; }
    public string Role { get; init; } = string.Empty;
    public int RoleId { get; init; }
    public string Login { get; init; } = string.Empty;
    public string PasswordHash { get; init; } = string.Empty;
};

