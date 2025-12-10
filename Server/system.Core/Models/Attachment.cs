using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Attachment
{
    public Attachment(long id, long orderId, int workerId, DateTime createAt, string? description)
    {
        Id = id;
        OrderId = orderId; 
        WorkerId = workerId; 
        CreatedAt = createAt; 
        Description = description;
    }

    public long Id { get; set; }
    public long OrderId { get; set; }
    public int WorkerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Description { get; set; } = string.Empty;


    public static (Attachment? attachment, List<string>? strings) Create(long id, long orderId, int workerId, DateTime createdAt, string? description)
    {
        var errors = new List<string>();

        var IdError = DomainValidator.ValidateId(id, "id");
        if (IdError != null) errors.Add(IdError);

        var orderIdError = DomainValidator.ValidateId(workerId, "workerId");
        if (orderIdError != null) errors.Add(orderIdError);

        var createAtError = DomainValidator.ValidateDate(createdAt, "orderIdError");
        if (createAtError != null) errors.Add(createAtError);

        var descriptionError = DomainValidator.ValidateString(description, ValidationConstants.MAX_DESCRIPTION_LENGTH, "description");
        if (descriptionError != null) errors.Add(descriptionError);

        if (errors.Any())
            return (null, errors);

        var attachment = new Attachment(id, orderId, workerId, createdAt, description);

        return (attachment, new List<string>());
    }
}
