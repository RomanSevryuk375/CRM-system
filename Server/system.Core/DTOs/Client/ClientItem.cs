namespace CRMSystem.Core.DTOs.Client;

public record ClientItem
(
    long Id,
    long UserId,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email
);
