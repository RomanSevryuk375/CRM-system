using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Work
{
    public Work(long id, string title, string categoty, string description, decimal standartTime)
    {
        Id = id;
        Title = title;
        Category = categoty;
        Description = description;
        StandardTime = standartTime;
    }
    public long Id { get; }
    public string Title { get; }
    public string Category { get; } 
    public string Description { get;} 
    public decimal StandardTime { get; }

    public static (Work? work, List<string> errors) Create(long id, string title, string categoty, string description, decimal standartTime)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var titleError = DomainValidator.ValidateString(title, ValidationConstants.MAX_NAME_LENGTH, "title");
        if (!string.IsNullOrEmpty(titleError)) errors.Add(titleError);

        var categoryError = DomainValidator.ValidateString(categoty, ValidationConstants.MAX_CATEGORY_LENGTH, "category");
        if (!string.IsNullOrEmpty(categoryError)) errors.Add(categoryError);

        var descriptionError = DomainValidator.ValidateString(description, ValidationConstants.MAX_DESCRIPTION_LENGTH, "description");
        if (!string.IsNullOrEmpty(descriptionError)) errors.Add(descriptionError);

        var standardTimeError = DomainValidator.ValidateMoney(standartTime, "time");
        if (!string.IsNullOrEmpty(standardTimeError)) errors.Add(standardTimeError);

        if (errors.Any())
            return (null, errors);

        var work = new Work(id, title, categoty, description, standartTime);

        return (work, errors);
    }
}
