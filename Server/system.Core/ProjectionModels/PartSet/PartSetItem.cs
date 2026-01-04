namespace CRMSystem.Core.DTOs.PartSet;

public record PartSetItem
{
    public long Id { get; init; }
    public long? OrderId { get; init; }
    public string Position { get; init; } = string.Empty;
    public long PositionId { get; init; }
    public long? ProposalId { get; init; }
    public decimal Quantity { get; init; }
    public decimal SoldPrice { get; init; }
};
