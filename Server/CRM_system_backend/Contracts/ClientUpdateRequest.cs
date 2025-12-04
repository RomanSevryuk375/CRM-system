namespace CRM_system_backend.Contracts;

public record ClientUpdateRequest
(
    string? Name,
    string? Surname,
    string? Email,
    string? PhoneNumber
);
