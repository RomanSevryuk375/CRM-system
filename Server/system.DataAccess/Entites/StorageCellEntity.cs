namespace CRMSystem.DataAccess.Entites;

public class StorageCellEntity 
{
    public int Id { get; set; }
    public string Rack { get; set; } = string.Empty;
    public string Shelf { get; set; } = string.Empty;

    public ICollection<PositionEntity> Positions { get; set; } = new List<PositionEntity>();
}
