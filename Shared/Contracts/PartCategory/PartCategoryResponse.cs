namespace Shared.Contracts.PartCategory;

public record PartCategoryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
};
