using System.ComponentModel.DataAnnotations.Schema;

namespace CRMSystem.DataAccess.Entites;

public class BillEntity
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal Amount { get; set; }
    public DateOnly? ActualBillDate { get; set; }
    [NotMapped] public DateTime LastBillDate => CreatedAt.AddDays(14);

    public BillStatusEntity? Status { get; set; }
    public OrderEntity? Order { get; set; }
    public ICollection<PaymentNoteEntity> Payments { get; set; } = new HashSet<PaymentNoteEntity>();
}
