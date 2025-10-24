namespace CRMSystem.DataAccess.Entites;

public class OrderEntity
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    public int CarId { get; set; }

    public DateTime Date { get; set; }

    public string Priority { get; set; } = string.Empty;

    public CarEntity? Car { get; set; }

    public StatusEntity? Status { get; set; }

    public ICollection<WorkEntity> Works { get; set; } = new HashSet<WorkEntity>();


    public ICollection<WorkProposalEntity> WorkProposals { get; set; }  = new HashSet<WorkProposalEntity>();

    public ICollection<BillEntity> Bills { get; set; } = new List<BillEntity>();

    public ICollection<RepairHistoryEntity> RepairHistories { get; set; } = new List<RepairHistoryEntity>();

    public ICollection<UsedPartEntity> UsedParts { get; set; } = new HashSet<UsedPartEntity>();
}
