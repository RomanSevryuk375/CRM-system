using CRMSystem.Core.Enums;

namespace CRMSystem.DataAccess.Entites;

public class UserEntity
{
    public int UserId { get; set;  }

    public int UserRoleId { get; set; }

    public string UserLogin { get; set; } = string.Empty;

    public string UserPasswordHash { get; set; } = string.Empty;

}
