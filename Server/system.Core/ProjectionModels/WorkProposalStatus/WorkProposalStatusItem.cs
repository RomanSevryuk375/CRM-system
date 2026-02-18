namespace CRMSystem.Core.ProjectionModels.WorkProposalStatus;

public record WorkProposalStatusItem
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
};
