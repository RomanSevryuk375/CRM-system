namespace CRMSystem.Core.ProjectionModels.Client;

public record ClientUpdateModel
(
    string? Name,
    string? Surname,
    string? PhoneNumber,
    string? Email
);
