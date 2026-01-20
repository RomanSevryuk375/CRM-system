namespace Shared.Contracts.Client;

public record ClientsRequest
(
    long Id,
    long UserId,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email
);
