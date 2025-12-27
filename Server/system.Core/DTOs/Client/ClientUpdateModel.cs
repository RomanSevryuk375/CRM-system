namespace CRMSystem.Core.DTOs.Client;

public record ClientUpdateModel
(
    string? name,
    string? surname,
    string? phoneNumber,
    string? email
);
