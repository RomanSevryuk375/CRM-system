namespace CRMSystem.Core.ProjectionModels.Client;

public record ClientCreateModel
(
    long UserId,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email);