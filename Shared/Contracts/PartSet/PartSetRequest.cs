namespace Shared.Contracts.PartSet;

public record PartSetRequest
(
    long? OrderId,
    long PositionId,
    long? ProposalId,
    decimal Quantity,
    decimal SoldPrice
);
