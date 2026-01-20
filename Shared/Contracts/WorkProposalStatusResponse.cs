namespace Shared.Contracts;

public record WorkProposalStatusResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
