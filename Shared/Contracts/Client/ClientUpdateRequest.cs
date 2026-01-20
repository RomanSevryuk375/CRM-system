namespace Shared.Contracts.Client;

public record ClientUpdateRequest
(
    string? Name,
    string? Surname,
    string? PhoneNumber,
    string? Email
);
