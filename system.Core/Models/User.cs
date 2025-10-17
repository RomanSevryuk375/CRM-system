using CRMSystem.Core.Enums;
using System.Text.RegularExpressions;

namespace CRMSystem.Core.Models;

public class User
{
    public const int MAX_LOGIN_LENGHT = 256;
    public const int MIN_PASSWORD_LENGHT = 6;
    public const int MAX_PASSWORD_LENGHT = 128;
    private User(int id, int roleId, string login, string passwordHash)
    {
        Id = id;
        RoleId = roleId;
        Login = login;
        PasswordHash = passwordHash;
    }
    public int Id { get; }

    public int RoleId { get; }

    public string Login {  get; }

    public string PasswordHash { get; }

    public static (User user, string error) Create(int id, int roleId, string login, string PasswordHash)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(login))
        {
            error = "Login can't be empty";
        }
        else if (login.Length > MAX_LOGIN_LENGHT)
        {
            error = $"Login can't be longer than {MAX_LOGIN_LENGHT} symbols";
        }
        else if (!Regex.IsMatch(login, @"^[a-zA-Z0-9_]+$"))
        {
            error = "Login can contain only letters, numbers and underscores";
        }

        if (string.IsNullOrWhiteSpace(PasswordHash))
        {
            error = "Password hash can't be empty";
        }
        else if (PasswordHash.Length > MAX_PASSWORD_LENGHT)
        {
            error = $"Password hash can't be longer than {MAX_PASSWORD_LENGHT} symbols";
        }
        else if (PasswordHash.Length < MIN_PASSWORD_LENGHT)
        {
            error = $"Password hash can't be shoter than {MIN_PASSWORD_LENGHT} symbols";
        }

            var user = new User(id, roleId, login, PasswordHash);

        return (user, string.Empty);
    }
}
