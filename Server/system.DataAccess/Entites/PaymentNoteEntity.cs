using CRMSystem.Core.Enums;

namespace CRMSystem.DataAccess.Entites;

public class PaymentNoteEntity
{
    public long Id { get; set; }
    public long BillId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethodEnum MethodId { get; set; }

    public BillEntity? Bill { get; set; }
    public PaymentMethodEntity? Method { get; set; }
}
