using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Supply
{
    public Supply(long id, int supplierId, DateOnly date)
    {
        Id = id;
        SupplierId = supplierId;
        Date = date;
    }
    public long Id { get; }
    public int SupplierId { get; }
    public DateOnly Date { get; }

    public static (Supply? supply, List<string> errors) Create (long id, int supplierId, DateOnly date)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var supplierIdError = DomainValidator.ValidateId(supplierId, "supplierId");
        if (!string.IsNullOrEmpty(supplierIdError)) errors.Add(supplierIdError);

        var dateError = DomainValidator.ValidateDate(date, "date");
        if (!string.IsNullOrEmpty(dateError)) errors.Add(dateError);

        if (errors.Any()) 
            return (null,  errors);

        var supply = new Supply(id, supplierId, date);

        return (supply, new List<string>());
    }
}
