namespace CRMSystem.Core.DTOs.Client;

public record ClientItem
(
    long id,
    long userId,
    string name,
    string surname,
    string phoneNumber,
    string email
);
