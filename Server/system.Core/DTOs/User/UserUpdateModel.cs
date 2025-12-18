namespace CRMSystem.Core.DTOs.User;

public record UserUpdateModel
(
    string login,
    string passwordHash
);
