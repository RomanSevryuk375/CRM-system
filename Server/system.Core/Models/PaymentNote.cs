using CRMSystem.Core.Validation;
using Shared.Enums;

namespace CRMSystem.Core.Models;

public class PaymentNote
{
    private PaymentNote(long id, long billId, DateTime date, decimal amount, PaymentMethodEnum methodId)
    {
        Id = id;
        BillId = billId;
        Date = date; 
        Amount = amount;
        MethodId = methodId;
    }
    public long Id { get; }
    public long BillId { get; }
    public DateTime Date { get; }
    public decimal Amount { get; }
    public PaymentMethodEnum MethodId { get; }

    public static (PaymentNote? paymentNote, List<string> errors) Create(long id, long billId, DateTime date, decimal amount, PaymentMethodEnum methodId)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var billIdError = DomainValidator.ValidateId(billId, "billId");
        if (!string.IsNullOrEmpty(billIdError)) errors.Add(billIdError);

        var methodIdError = DomainValidator.ValidateId(methodId, "methodId");
        if (!string.IsNullOrEmpty(methodIdError)) errors.Add(methodIdError);

        var dateError = DomainValidator.ValidateDate(date, "date");
        if (!string.IsNullOrEmpty(dateError)) errors.Add(dateError);

        var amountError = DomainValidator.ValidateMoney(amount, "amount");
        if (!string.IsNullOrEmpty(amountError)) errors.Add(amountError);

        if (errors.Any())
            return (null, errors);

        var paymentJournal = new PaymentNote(id, billId, date, amount, methodId);

        return (paymentJournal, new List<string>());
    }
}
