namespace CRMSystem.Core.DTOs.PartSet;

public record PartSetItem
(
    long Id,
    long? OrderId,
    string Position,
    long PositionId,
    long? ProposalId,
    decimal Quantity,
    decimal SoldPrice
);
