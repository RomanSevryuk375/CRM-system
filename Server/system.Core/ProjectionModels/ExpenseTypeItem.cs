namespace CRMSystem.Core.ProjectionModels;

public record ExpenseTypeItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
