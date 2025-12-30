namespace CRM_system_backend.Contracts.Client;

public record ClientsRequest
(
    long id, 
    long userId, 
    string name, 
    string surname, 
    string phoneNumber, 
    string email
);
