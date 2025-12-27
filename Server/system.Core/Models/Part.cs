using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Part
{
    public Part(long id, int categoryId, string? oemArticle, string? manufacturerArticle, string internalArticle, string? description, string name, string manufacturer, string applicability)
    {
        Id = id;
        CategoryId = categoryId;
        OEMArticle = oemArticle;
        ManufacturerArticle = manufacturerArticle;
        Manufacturer = manufacturer;
        InternalArticle = internalArticle;
        Description = description;
        Name = name;
        Applicability = applicability;
    }

    public long Id { get; }
    public int CategoryId { get; }
    public string? OEMArticle { get; }
    public string? ManufacturerArticle { get; } 
    public string InternalArticle { get; }
    public string? Description { get; } 
    public string Name { get; } 
    public string Manufacturer { get; } 
    public string Applicability { get; } 

    public static (Part? part, List<string> errors) Create(long id, int categoryId, string? oemArticle, string? manufacturerArticle, string internalArticle, string? description, string name, string manufacturer, string applicability)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var categoryIdError = DomainValidator.ValidateId(categoryId, "category");
        if (!string.IsNullOrEmpty(categoryIdError)) errors.Add(categoryIdError);

        var InternalAarticleError = DomainValidator.ValidateString(internalArticle, ValidationConstants.MAX_ARTICLE_LENGTH, "internalArticle");
        if (!string.IsNullOrEmpty(InternalAarticleError)) errors.Add(InternalAarticleError);

        var nameError = DomainValidator.ValidateString(name, ValidationConstants.MAX_NAME_LENGTH, "name");
        if (!string.IsNullOrEmpty(nameError)) errors.Add(nameError);

        var manufacturerError = DomainValidator.ValidateString(manufacturer, ValidationConstants.MAX_NAME_LENGTH, "manufacturer");
        if (!string.IsNullOrEmpty(manufacturerError)) errors.Add(manufacturerError);

        var applicabilityError = DomainValidator.ValidateString(applicability, ValidationConstants.MAX_DESCRIPTION_LENGTH, "applicability");
        if (!string.IsNullOrEmpty(applicabilityError)) errors.Add(applicabilityError);

        if (errors.Any())
            return (null, errors);

        var part = new Part(id, categoryId, oemArticle, manufacturerArticle, internalArticle, description, name, manufacturer, applicability);

        return (part, new List<string>());
    }
}
