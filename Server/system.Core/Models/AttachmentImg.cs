// Ignore Spelling: Img

using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class AttachmentImg
{
    private AttachmentImg(long id, long attachmentId, string filePath, string? description)
    {
        Id = id;
        AttachmentId = attachmentId;
        FilePath = filePath;
        Description = description;
    }

    public long Id { get; set; }
    public long AttachmentId { get; }
    public string FilePath { get; }
    public string? Description { get; }

    public static (AttachmentImg? attachmentImg, List<string>? errors) Create(
        long id, long attachmentId, string filePath, string? description)
    {
        var errors = new List<string>();

        var idError = DomainValidator
            .ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError))
        {
            errors.Add(idError);
        }

        var acceptanceIdError = DomainValidator
            .ValidateId(attachmentId, "acceptanceId");
        if (!string.IsNullOrEmpty(acceptanceIdError))
        {
            errors.Add(acceptanceIdError);
        }

        var filePathError = DomainValidator
            .ValidateString(filePath, ValidationConstants.MAX_PATH_LENGTH, "filePath");
        if (!string.IsNullOrEmpty(filePathError))
        {
            errors.Add(filePathError);
        }

        var descriptionError = DomainValidator
            .ValidateString(description, ValidationConstants.MAX_DESCRIPTION_LENGTH, "description");
        if (!string.IsNullOrEmpty(descriptionError))
        {
            errors.Add(descriptionError);
        }

        if (errors.Any())
        {
            return (null, errors);
        }

        var attachmentImg = new AttachmentImg(
            id, 
            attachmentId, 
            filePath, 
            description);

        return (attachmentImg, []);
    }
}
