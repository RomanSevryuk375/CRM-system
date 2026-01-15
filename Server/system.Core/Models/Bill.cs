using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Bill
{
    private Bill(long id, long orderId, BillStatusEnum statusId, DateTime createdAt, decimal amount, DateOnly? actualBillDate)
    {
        Id = id;
        OrderId = orderId;
        StatusId = statusId;
        CreatedAt = createdAt;
        Amount = amount;
        ActualBillDate = actualBillDate;
    }

    public void SetOrderId(long orderId)
    {
        if(orderId <= 0) throw new ConflictException(nameof(orderId));
        OrderId = orderId;
    }

    public long Id { get; }
    public long OrderId { get; private set; }
    public BillStatusEnum StatusId { get; }
    public DateTime CreatedAt { get; }
    public decimal Amount { get; }
    public DateOnly? ActualBillDate { get; }
    public DateTime LastBillDate => CreatedAt.AddDays(14);

    public static (Bill? bill, List<string> errors ) Create (long id, long orderId, BillStatusEnum statusId, DateTime createdAt, decimal amount, DateOnly? actualBillDate)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var orderIdError = DomainValidator.ValidateId(orderId, "orderId");
        if (!string.IsNullOrEmpty(orderIdError)) errors.Add(orderIdError); 

        var statusIdError = DomainValidator.ValidateId(statusId, "statusId");
        if (!string.IsNullOrEmpty(statusIdError)) errors.Add(statusIdError);

        var createdAtError = DomainValidator.ValidateDate(createdAt, "createdAt");
        if (!string.IsNullOrEmpty(createdAtError)) errors.Add(createdAtError);

        var amountError = DomainValidator.ValidateMoney(amount, "amount");
        if (!string.IsNullOrEmpty(amountError)) errors.Add(amountError);

        if (statusId == BillStatusEnum.Paid && !actualBillDate.HasValue)
        {
            errors.Add("Actual closing date must be set for paid bills");
        }

        else if (actualBillDate.HasValue)
        {
            var actualDateError = DomainValidator.ValidateDate(actualBillDate, "Actual bill date"); 
            if (!string.IsNullOrEmpty(actualDateError)) errors.Add(actualDateError);

            var actualDateRangeError = DomainValidator.ValidateDateRange(createdAt, actualBillDate);
            if (!string.IsNullOrEmpty(actualDateRangeError)) errors.Add(actualDateRangeError);
        }

        if (errors.Any())
            return (null, errors);

        var bills = new Bill(id, orderId, statusId, createdAt, amount, actualBillDate);

        return (bills, new List<string>());
    }
}
