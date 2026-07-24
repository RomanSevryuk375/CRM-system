using CRM.Billing.Domain.Enums;
using CRM.Billing.Domain.ValueObjects;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Billing.Domain.Entities;

public sealed class Bill : AggregateRoot<BillId>, ISoftDeletable, IAuditable, IHasVersion
{
    private const int CriticalOffsetDays = 14;

    private Bill(
        BillId id,
        OrderId orderId,
        BillStatus status,
        Money amount,
        DateOnly? actualBillDate)
    {
        Id = id;
        OrderId = orderId;
        Status = status;
        Amount = amount;
        ActualBillDate = actualBillDate;
    }

#pragma warning disable CS8618
    private Bill() { }
#pragma warning restore CS8618

    private readonly List<PaymentNote> _paymentNotes = [];

    public OrderId OrderId { get; private set; }
    public BillStatus Status { get; private set; }
    public Money Amount { get; private set; }
    public DateOnly? ActualBillDate { get; private set; }
    public DateOnly LastBillDate => DateOnly.FromDateTime(CreatedAt.Date.AddDays(CriticalOffsetDays));
    public decimal TotalPaidAmount => _paymentNotes.Where(p => !p.IsDeleted).Sum(p => p.Amount.Value);

    public IReadOnlyList<PaymentNote> PaymentNotes => _paymentNotes.AsReadOnly();

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public Guid Version { get; private set; }
#pragma warning restore S1144

    public static Result<Bill> Create(
        BillId id,
        OrderId orderId,
        BillStatus status,
        decimal amount,
        DateOnly? actualBillDate,
        DateOnly today)
    {
        List<Error> errors = [];

        Result<Money> moneyResult = Money.Create(amount);
        if (moneyResult.IsFailure)
        {
            errors.Add(moneyResult.Error);
        }

        if (status == BillStatus.Paid &&
            !actualBillDate.HasValue)
        {
            errors.Add(Error.Validation<Bill>(
                "Actual closing date must be set for paid bills."));
        }
        else if (actualBillDate.HasValue &&
                 actualBillDate > today)
        {
            errors.Add(Error.Validation<Bill>(
                "Actual bill date can not be in the future."));
        }

        if (errors.Count != 0)
        {
            return Result<Bill>.Failure(Error.Validation<Bill>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        Bill bill = new(id, orderId, status, moneyResult.Value, actualBillDate);

        bill.IncrementVersion();

        return Result<Bill>.Success(bill);
    }

    public Result CloseByUser(DateOnly today)
    {
        if (Status == BillStatus.Paid)
        {
            return Result.Failure(Error.Conflict<Bill>(
                "The bill is already paid."));
        }

        ActualBillDate = today;
        Status = BillStatus.Paid;

        IncrementVersion();

        return Result.Success();
    }

    public Result AddPaymentNote(
        PaymentNoteId paymentId,
        decimal paymentAmount,
        PaymentMethod method,
        DateTimeOffset paymentDate,
        DateTimeOffset today)
    {
        if (Status == BillStatus.Paid)
        {
            return Result.Failure(Error.Conflict<Bill>(
                "Cannot add payment to an already paid bill."));
        }

        Result<Money> moneyResult = Money.Create(paymentAmount);
        if (moneyResult.IsFailure)
        {
            return Result.Failure(moneyResult.Error);
        }

        if (paymentDate > today)
        {
            return Result.Failure(Error.Validation<PaymentNote>(
                "Payment date cannot be in the future."));
        }

        if (TotalPaidAmount + paymentAmount > Amount.Value)
        {
            return Result.Failure(Error.Validation<Bill>(
                "Payment amount exceeds the remaining bill balance."));
        }

        PaymentNote note = new(paymentId, Id, paymentDate, moneyResult.Value, method);

        _paymentNotes.Add(note);

        RecalculateStatus(DateOnly.FromDateTime(today.Date));
        IncrementVersion();

        return Result.Success();
    }

    public Result RemovePaymentNote(PaymentNoteId paymentNoteId, DateTimeOffset today)
    {
        PaymentNote? note = _paymentNotes.Find(x => x.Id == paymentNoteId);
        if (note is null)
        {
            return Result.Failure(Error.NotFound<PaymentNote>(
                "Payment note not found in this bill."));
        }

        _paymentNotes.Remove(note);

        RecalculateStatus(DateOnly.FromDateTime(today.Date));
        IncrementVersion();

        return Result.Success();
    }

    private void RecalculateStatus(DateOnly today)
    {
        decimal paid = TotalPaidAmount;

        if (paid == 0)
        {
            Status = BillStatus.Unpaid;
            ActualBillDate = null;
        }
        else if (paid >= Amount.Value)
        {
            Status = BillStatus.Paid;
            ActualBillDate = today;
        }
        else
        {
            Status = BillStatus.PartiallyPaid;
            ActualBillDate = null;
        }
    }

    private void IncrementVersion()
    {
        Version = Guid.NewGuid();
    }
}