using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class AttachmentImg
{
    public AttachmentImg(long id, long attachmentId, string filePath, string? description)
    {
        Id = id;
        AttachmentId = attachmentId;
        FilePath = filePath;
        Description = description;
    }

    public long Id { get; set; }
    public long AttachmentId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public static (AttachmentImg? attachmentImg, List<string>? errors) Create(long id, long attachmentId, string filePath, string? description)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var acceptanceIdError = DomainValidator.ValidateId(attachmentId, "acceptanceId");
        if (!string.IsNullOrEmpty(acceptanceIdError)) errors.Add(acceptanceIdError);

        var filePathError = DomainValidator.ValidateString(filePath, ValidationConstants.MAX_PATH_LENGTH, "filePath");
        if (!string.IsNullOrEmpty(filePathError)) errors.Add(filePathError);

        var descriptionError = DomainValidator.ValidateString(description, ValidationConstants.MAX_DESCRIPTION_LENGTH, "description");
        if (!string.IsNullOrEmpty(descriptionError)) errors.Add(descriptionError);

        if (errors.Any())
            return (null, errors);

        var attachmentImg = new AttachmentImg(id, attachmentId, filePath, description);

        return (attachmentImg, new List<string>());
    }
}
