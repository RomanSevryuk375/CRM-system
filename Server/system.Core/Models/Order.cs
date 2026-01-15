using CRMSystem.Core.Enums;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Order
{
    private Order(long id, OrderStatusEnum statusId, long carId, DateOnly date, OrderPriorityEnum priorityId)
    {
        Id = id;
        StatusId = statusId;
        CarId = carId;
        Date = date;
        PriorityId = priorityId;
    }
    public long Id { get; }
    public OrderStatusEnum StatusId { get; }
    public long CarId { get; }
    public DateOnly Date { get; }
    public OrderPriorityEnum PriorityId { get; }

    public static (Order? order, List<string> errors) Create(long id, OrderStatusEnum statusId, long carId, DateOnly date, OrderPriorityEnum priorityId)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var statusIdError = DomainValidator.ValidateId(statusId, "status");
        if (!string.IsNullOrEmpty(statusIdError)) errors.Add(statusIdError);

        var carIdError = DomainValidator.ValidateId(carId, "carId");
        if (!string.IsNullOrEmpty(carIdError)) errors.Add(carIdError);

        var prioprityIdError = DomainValidator.ValidateId(priorityId, "priorityId");
        if (!string.IsNullOrEmpty(prioprityIdError)) errors.Add(prioprityIdError);

        var dateError = DomainValidator.ValidateDate(date, "date");
        if (!string.IsNullOrEmpty(dateError)) errors.Add(dateError);

        if (errors.Any())
            return (null,  errors);

        var order = new Order(id, statusId, carId, date, priorityId);

        return (order, new List<string>());
     }
}
