namespace CRMSystem.Core.ProjectionModels.ExpenseType;

public record ExpenseTypeItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
