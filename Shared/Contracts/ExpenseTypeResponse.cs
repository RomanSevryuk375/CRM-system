namespace Shared.Contracts;

public record ExpenseTypeResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
