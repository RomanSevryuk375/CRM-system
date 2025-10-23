namespace CRMSystem.DataAccess.Entites;

public class PaymentJournalEntity
{
    public int Id { get; set; }

    public int BillId { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }

    public string Method { get; set; } = string.Empty;

    public BillEntity? Bill { get; set; }
}
