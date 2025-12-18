namespace CRMSystem.Core.DTOs.User;

public record UserItem
(
    long id,
    string role, 
    string login, 
    string passwordHash
);

