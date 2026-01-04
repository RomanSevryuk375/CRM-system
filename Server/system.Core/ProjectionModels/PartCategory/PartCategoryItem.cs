namespace CRMSystem.Core.DTOs.PartCategory;

public record PartCategoryItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
};
