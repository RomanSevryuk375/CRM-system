namespace CRM_system_backend.Contracts.PartSet;

public record PartSetResponse
{
    public long Id { get; init; }
    public long? OrderId { get; init; }
    public string Position { get; init; } = string.Empty;
    public long PositionId { get; init; }
    public long? ProposalId { get; init; }
    public decimal Quantity { get; init; }
    public decimal SoldPrice { get; init; }
};
