using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class AcceptanceImg
{
    public AcceptanceImg(int id, int acceptanceId, string filePath, string? description)
    {
        Id = id;
        Description = description;
        FilePath = filePath;
        Description = description;
    }
    public long Id { get; set; }
    public long AcceptanceId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public static (AcceptanceImg? acceptanceImg, List<string>? errors) Create(int id, int acceptanceId, string filePath, string? description)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var acceptanceIdError = DomainValidator.ValidateId(acceptanceId, "acceptanceId");
        if (!string.IsNullOrEmpty(acceptanceIdError)) errors.Add(acceptanceIdError);

        var filePathError = DomainValidator.ValidateString(filePath, ValidationConstants.MAX_PATH_LENGTH, "filePath");
        if (!string.IsNullOrEmpty(filePathError)) errors.Add(filePathError);

        var descriptionError = DomainValidator.ValidateString(description, ValidationConstants.MAX_DESCRIPTION_LENGTH, "description");
        if (!string.IsNullOrEmpty(descriptionError)) errors.Add(descriptionError);

        if (errors.Any())
            return (null, errors);

        var acceptanceImg = new AcceptanceImg(id, acceptanceId, filePath, description);

        return (acceptanceImg, new List<string>());
    }
}
