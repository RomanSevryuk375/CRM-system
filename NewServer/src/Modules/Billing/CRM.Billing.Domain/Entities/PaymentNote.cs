using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.ValueObjects;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;

namespace CRM.Billing.Domain.Entities;

public sealed class PaymentNote : IEntity<PaymentNoteId>, IAuditable, ISoftDeletable
{
    internal PaymentNote(
        PaymentNoteId id,
        BillId billId,
        DateTimeOffset date,
        Money amount,
        PaymentMethod methodId)
    {
        Id = id;
        BillId = billId;
        Date = date;
        Amount = amount;
        Method = methodId;
    }

#pragma warning disable CS8618
    private PaymentNote() { }
#pragma warning restore CS8618

    public PaymentNoteId Id { get; private set; }
    public BillId BillId { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public Money Amount { get; private set; }
    public PaymentMethod Method { get; private set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
}

