using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Guarantee
{
    public Guarantee(long id, long orderId, DateOnly dateStart, DateOnly dateEnd, string? description, string terms)
    {
        Id = id;
        OrderId = orderId; 
        DateStart = dateStart; 
        DateEnd = dateEnd; 
        Description = description; 
        Terms = terms;
    }

    public long Id { get; }
    public long OrderId { get; }
    public DateOnly DateStart { get; }
    public DateOnly DateEnd { get; }
    public string? Description { get; } 
    public string Terms { get; } 

    public static (Guarantee? guarantee, List<string> errors) Create(long id, long orderId, DateOnly dateStart, DateOnly dateEnd, string? description, string terms)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var orderIdError = DomainValidator.ValidateId(orderId, "orderId");
        if (!string.IsNullOrEmpty(orderIdError)) errors.Add(orderIdError);

        var dateRangeError = DomainValidator.ValidateDateRange(dateStart, dateEnd);
        if (!string.IsNullOrEmpty(dateRangeError)) errors.Add(dateRangeError);

        var termsError = DomainValidator.ValidateString(terms, ValidationConstants.MAX_DESCRIPTION_LENGTH, "terms");
        if (!string.IsNullOrEmpty(termsError)) errors.Add(termsError);

        if (errors.Any())
            return (null, errors);

        var guarantee = new Guarantee(id, orderId, dateStart, dateEnd, description, terms);

        return (guarantee, new List<string>());
    }
}
