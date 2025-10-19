namespace CRMSystem.DataAccess.Entites;

public class RoleEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}
