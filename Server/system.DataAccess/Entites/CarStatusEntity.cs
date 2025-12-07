namespace CRMSystem.DataAccess.Entites;

public class CarStatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<CarEntity> Cars { get; set; } = new List<CarEntity>();
}
