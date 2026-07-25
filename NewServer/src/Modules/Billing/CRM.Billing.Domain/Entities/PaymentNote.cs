using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;

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

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144
}

