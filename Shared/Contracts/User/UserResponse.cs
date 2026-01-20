namespace Shared.Contracts.User;

public record UserResponse
(
    long Id,
    string Role,
    int RoleId,
    string Login,
    string PasswordHash
);
