using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRMSystem.Core.Models;

public class User
{
    public const int MAX_LOGIN_LENGTH = 128;
    public const int MAX_PASSWORD_LENGTH = 256;
    public const int MIN_PASSWORD_LENGTH = 6;
    private User(int id, int roleId, string login, string passwordHash)
    {
        Id = id;
        RoleId = roleId;    
        Login = login;
        PasswordHash = passwordHash;
    }

    public int Id { get; }

    public int RoleId { get; }

    public string Login { get; } = string.Empty;

    public string PasswordHash { get; } = string.Empty;

    public static (User user, string? error) Create(int id, int roleId, string login, string passwordHash)
    {
        var error = string.Empty;

        if (string.IsNullOrWhiteSpace(login))
        {
            error = "Login can't be empty";
        }
        else if (login.Length > MAX_LOGIN_LENGTH)
        {
            error = $"Login can't be longer than {MAX_LOGIN_LENGTH} symbols";
        }

        if (string.IsNullOrWhiteSpace(passwordHash)) 
        {
            error = "Password can't be empty";
        }
        else if (passwordHash.Length > MAX_PASSWORD_LENGTH)
        {
            error = $"Password can't be longer than {MAX_PASSWORD_LENGTH} symbols";
        }
        else if (passwordHash.Length < MIN_PASSWORD_LENGTH)
        {
            error = $"Password can't be shoter than {MIN_PASSWORD_LENGTH} symbols";
        }

            var user = new User(id, roleId, login, passwordHash);

        return (user, error); 
    }
}
