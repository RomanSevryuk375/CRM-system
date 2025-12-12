using CRMSystem.Core.Constants;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Tax
{
    public Tax(int id, string name, decimal rate, TaxTypeEnum typeId)
    {
        Id = id;
        Name = name;
        Rate = rate;
        TypeId = typeId;
    }

    public int Id { get; }
    public string Name { get; } 
    public decimal Rate { get; }
    public TaxTypeEnum TypeId { get; } 

    public static (Tax? tax, List<string> errors) Create (int id, string name, decimal rate, TaxTypeEnum typeId)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var nameError = DomainValidator.ValidateString(name, ValidationConstants.MAX_TYPE_NAME, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        var rateError = DomainValidator.ValidateMoney(rate, "rate");
        if (!string.IsNullOrEmpty(rateError)) errors.Add(rateError);

        var typeIdError = DomainValidator.ValidateId(typeId, "typeId");
        if (!string.IsNullOrEmpty(typeIdError)) errors.Add(typeIdError);

        if(errors.Any())
            return (null, errors);

        var tax = new Tax(id, name, rate, typeId);

        return (tax, new List<string>());
    }
}
