namespace CRMSystem.Core.ProjectionModels.Guarantee;

public record GuaranteeCreateModel
(
    long OrderId,
    DateOnly DateStart, 
    DateOnly DateEnd, 
    string? Description, 
    string Terms);