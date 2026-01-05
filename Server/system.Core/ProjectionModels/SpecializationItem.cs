namespace CRMSystem.Core.DTOs;

public record SpecializationItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
