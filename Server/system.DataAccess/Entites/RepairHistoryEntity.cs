namespace CRMSystem.DataAccess.Entites;

public class RepairHistoryEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int CarId { get; set; }

    public DateTime WorkDate { get; set; }

    public decimal ServiceSum { get; set; }

    public CarEntity? Car { get; set; }

    public OrderEntity? Order { get; set; }
}
