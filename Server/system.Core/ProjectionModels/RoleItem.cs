namespace CRMSystem.Core.DTOs;

public record RoleItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};

