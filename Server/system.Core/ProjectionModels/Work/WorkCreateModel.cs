namespace CRMSystem.Core.ProjectionModels.Work;

public record WorkCreateModel
(
    string Title,
    string Category,
    string Description,
    decimal StandardTime);