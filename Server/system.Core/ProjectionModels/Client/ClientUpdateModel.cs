namespace CRMSystem.Core.DTOs.Client;

public record ClientUpdateModel
(
    string? Name,
    string? Surname,
    string? PhoneNumber,
    string? Email
);
