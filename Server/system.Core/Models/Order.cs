using CRMSystem.Core.Validation;
using Shared.Enums;

namespace CRMSystem.Core.Models;

public class Order
{
    private Order(
        long id,
        OrderStatusEnum statusId,
        long carId,
        DateOnly date,
        string? orderPdfFileName,
        string? orderAgreementPdfFileName,
        OrderPriorityEnum priorityId)
    {
        Id = id;
        StatusId = statusId;
        CarId = carId;
        Date = date;
        OrderPdfFileName = orderPdfFileName;
        OrderAgreementPdfFileName = orderAgreementPdfFileName;
        PriorityId = priorityId;
    }
    public long Id { get; }
    public OrderStatusEnum StatusId { get; }
    public long CarId { get; }
    public DateOnly Date { get; }
    public string? OrderPdfFileName { get; } 
    public string? OrderAgreementPdfFileName { get; } 
    public OrderPriorityEnum PriorityId { get; }

    public static (Order? order, List<string>? errors) Create(
        long id, 
        OrderStatusEnum statusId, 
        long carId,
        DateOnly date,
        string? orderPdfFileName,
        string? orderAgreementPdfFileName,
        OrderPriorityEnum priorityId)
    {
        var errors = new List<string>();

        var idError = DomainValidator
            .ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError))
        {
            errors.Add(idError);
        }

        var statusIdError = DomainValidator
            .ValidateId(statusId, "status");
        if (!string.IsNullOrEmpty(statusIdError))
        {
            errors.Add(statusIdError);
        }

        var carIdError = DomainValidator
            .ValidateId(carId, "carId");
        if (!string.IsNullOrEmpty(carIdError))
        {
            errors.Add(carIdError);
        }

        var priorityIdError = DomainValidator
            .ValidateId(priorityId, "priorityId");
        if (!string.IsNullOrEmpty(priorityIdError))
        {
            errors.Add(priorityIdError);
        }

        var dateError = DomainValidator
            .ValidateDateEmpty(date, "date");
        if (!string.IsNullOrEmpty(dateError))
        {
            errors.Add(dateError);
        }
        
        var orderFileNameError = DomainValidator
            .ValidateString(orderPdfFileName ,ValidationConstants.MAX_PATH_LENGTH, "filePath");
        if (!string.IsNullOrEmpty(orderFileNameError))
        {
            errors.Add(orderFileNameError);
        }
        
        var orderAgreementFileNameError = DomainValidator
            .ValidateString(orderAgreementPdfFileName, ValidationConstants.MAX_PATH_LENGTH, "filePath");
        if (!string.IsNullOrEmpty(orderAgreementFileNameError))
        {
            errors.Add(orderAgreementFileNameError);
        }

        if (errors.Any())
        {
            return (null, errors);
        }

        var order = new Order(id, statusId, carId, date, orderPdfFileName, orderAgreementPdfFileName, priorityId);

        return (order, []);
    }
}
