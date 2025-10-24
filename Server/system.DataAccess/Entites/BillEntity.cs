namespace CRMSystem.DataAccess.Entites;

public class BillEntity
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int StatusId { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public DateTime? ActualBillDate { get; set; }

    public DateTime LastBillDate => Date.AddDays(14);

    public StatusEntiy? Status { get; set; }

    public ICollection<PaymentJournalEntity> Payments { get; set; } = new List<PaymentJournalEntity>();
}
