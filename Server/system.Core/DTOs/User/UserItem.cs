namespace CRMSystem.Core.DTOs.User;

public record UserItem
(
    long id,
    string role,
    int roleId,
    string login, 
    string passwordHash
);

