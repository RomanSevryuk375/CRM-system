using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class User
{
    private User(long id, int roleId, string login, string passwordHash)
    {
        Id = id;
        RoleId = roleId;    
        Login = login;
        PasswordHash = passwordHash;
    }

    public long Id { get; }
    public int RoleId { get; }
    public string Login { get; } 
    public string PasswordHash { get; } 

    public static (User? user, List<string> errors) Create(long id, int roleId, string login, string passwordHash)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var roleIdError = DomainValidator.ValidateId(roleId, "RoleId");
        if (!string.IsNullOrEmpty(roleIdError)) errors.Add(roleIdError);

        var loginError = DomainValidator.ValidateString(login, ValidationConstants.MAX_NAME_LENGTH, "login");
        if (!string.IsNullOrEmpty(loginError)) errors.Add(loginError);

        var passwordError = DomainValidator.ValidateString(passwordHash, "password");
        if (!string.IsNullOrEmpty(passwordError)) errors.Add(passwordError);

        if(errors.Any())
            return (null, errors);

        var user = new User(id, roleId, login, passwordHash);

        return (user, new List<string>()); 
    }
}
