namespace CRMSystem.Core.DTOs.User;

public record UserItem
(
    long Id,
    string Role,
    int RoleId,
    string login,
    string PasswordHash
);

