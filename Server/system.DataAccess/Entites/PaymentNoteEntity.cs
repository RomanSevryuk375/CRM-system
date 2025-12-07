namespace CRMSystem.DataAccess.Entites;

public class PaymentNoteEntity
{
    public long Id { get; set; }
    public long BillId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = string.Empty;

    public BillEntity? Bill { get; set; }
}
