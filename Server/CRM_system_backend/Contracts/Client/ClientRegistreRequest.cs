namespace CRM_system_backend.Contracts.Client;

public record ClientRegistreRequest
(
    long userId,
    string name,
    string surname,
    string phoneNumber, 
    string email, 
    int roleId, 
    string login, 
    string password
);
