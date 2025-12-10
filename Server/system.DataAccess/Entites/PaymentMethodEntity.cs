namespace CRMSystem.DataAccess.Entites;

public class PaymentMethodEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<PaymentNoteEntity> PaymentNotes { get; set; } = new HashSet<PaymentNoteEntity>();
}
