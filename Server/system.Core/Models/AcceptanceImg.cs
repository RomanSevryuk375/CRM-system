using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class AcceptanceImg
{
    public AcceptanceImg(long id, long acceptanceId, string filePath, string? description)
    {
        Id = id;
        AcceptanceId = acceptanceId;
        FilePath = filePath;
        Description = description;
    }

    public long Id { get; set; }
    public long AcceptanceId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public static (AcceptanceImg? acceptanceImg, List<string>? errors) Create(long id, long acceptanceId, string filePath, string? description)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (idError != null) errors.Add(idError);

        var acceptanceIdError = DomainValidator.ValidateId(acceptanceId, "acceptanceId");
        if (acceptanceIdError != null) errors.Add(acceptanceIdError);

        var filePathError = DomainValidator.ValidateString(filePath, ValidationConstants.MAX_PATH_LENGTH, "filePath");
        if (filePathError != null) errors.Add(filePathError);

        var descriptionError = DomainValidator.ValidateString(description, ValidationConstants.MAX_DESCRIPTION_LENGTH, "description");
        if (descriptionError != null) errors.Add(descriptionError);

        if (errors.Any())
            return (null, errors);

        var acceptanceImg = new AcceptanceImg(id, acceptanceId, filePath, description);

        return (acceptanceImg, new List<string>());
    }
}
