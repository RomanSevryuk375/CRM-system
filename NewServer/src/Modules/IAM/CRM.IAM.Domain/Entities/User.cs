using CRM.IAM.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.IAM.Domain.Entities;

public sealed class User : AggregateRoot<UserId>, IAuditable, ISoftDeletable
{
    public const int MaxLoginLength = 128;
    public const int MaxPasswordHashLength = 256;

    private User(UserId id, Role role, string login, string passwordHash)
    {
        Id = id;
        Role = role;
        Login = login;
        PasswordHash = passwordHash;
    }

#pragma warning disable CS8618
    private User() { }
#pragma warning restore CS8618

    public Role Role { get; private set; }
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<User> Create(UserId id, Role role, string login, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            return Result<User>.Failure(Error.Validation<User>(Errors.LoginEmpty));
        }

        if (login.Length > MaxLoginLength)
        {
            return Result<User>.Failure(Error.Validation<User>(Errors.LoginTooLong));
        }

        if (string.IsNullOrWhiteSpace(passwordHash))
        {
            return Result<User>.Failure(Error.Validation<User>(Errors.PasswordEmpty));
        }

        if (passwordHash.Length > MaxPasswordHashLength)
        {
            return Result<User>.Failure(Error.Validation<User>(Errors.PasswordTooLong));
        }

        User user = new(id, role, login, passwordHash);
        user.IncrementVersion();

        return Result<User>.Success(user);
    }

    public Result ChangePassword(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
        {
            return Result.Failure(Error.Validation<User>(Errors.PasswordEmpty));
        }

        if (newPasswordHash.Length > MaxPasswordHashLength)
        {
            return Result.Failure(Error.Validation<User>(Errors.PasswordTooLong));
        }

        PasswordHash = newPasswordHash;
        IncrementVersion();

        return Result.Success();
    }

    public static class Errors
    {
        public const string LoginEmpty = "Login cannot be empty.";
        public static readonly string LoginTooLong = $"Login exceeds maximum length of {MaxLoginLength} characters.";
        public const string PasswordEmpty = "Password hash cannot be empty.";
        public static readonly string PasswordTooLong = $"Password hash exceeds maximum length of {MaxPasswordHashLength} characters.";
    }
}
