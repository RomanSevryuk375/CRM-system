namespace CRM_system_backend.Contracts.Client;

public record ClientUpdateRequest
(
    string? name,
    string? surname,
    string? phoneNumber,
    string? email
);
