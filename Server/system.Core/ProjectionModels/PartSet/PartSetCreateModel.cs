namespace CRMSystem.Core.ProjectionModels.PartSet;

public record PartSetCreateModel
(
    long? OrderId,
    long PositionId,
    long? ProposalId,
    decimal Quantity,
    decimal SoldPrice);