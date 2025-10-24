namespace CRMSystem.DataAccess.Entites;

public class OrderEntity
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    public int CarId { get; set; }

    public DateTime Date { get; set; }

    public string Priority { get; set; } = string.Empty;

    public ICollection<RepairHistoryEntity> RepairHistories { get; set; } = new List<RepairHistoryEntity>();

    public CarEntity? Car { get; set; }

    public StatusEntiy? Status { get; set; }

    public StatusEntiy? Entiy { get; set; }

    public ICollection<BillEntity> Bills { get; set; } = new List<BillEntity>();
}
