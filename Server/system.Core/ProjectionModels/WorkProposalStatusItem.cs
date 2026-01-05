namespace CRMSystem.Core.DTOs;

public record WorkProposalStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
