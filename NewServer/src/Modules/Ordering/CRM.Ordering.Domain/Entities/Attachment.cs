using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities;

public sealed class Attachment : AggregateRoot<AttachmentId>, ISoftDeletable, IAuditable
{
    private const int MaxFileSize = 15 * 1024 * 1024;
#pragma warning disable CS8618
    private Attachment() { }
#pragma warning restore CS8618

    private Attachment(
        AttachmentId id,
        OrderId orderId,
        WorkerId uploadedBy,
        string fileName,
        string filePath,
        string contentType,
        long fileSize,
        string? description)
    {
        Id = id;
        OrderId = orderId;
        UploadedBy = uploadedBy;
        FileName = fileName;
        FilePath = filePath;
        ContentType = contentType;
        FileSize = fileSize;
        Description = description;
    }

    public OrderId OrderId { get; private set; }
    public WorkerId UploadedBy { get; private set; }

    public string FileName { get; private set; }
    public string FilePath { get; private set; }
    public string ContentType { get; private set; }
    public long FileSize { get; private set; }
    public string? Description { get; private set; }


#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Attachment> Create(
        AttachmentId id,
        OrderId orderId,
        WorkerId uploadedBy,
        string fileName,
        string filePath,
        string contentType,
        long fileSize,
        string? description)
    {
        List<Error> errors = [];

        if (fileSize <= 0 || fileSize > MaxFileSize)
        {
            errors.Add(Error.Validation<Attachment>(
                "File size must be between 1 byte and 15 MB."));
        }

        string[] allowedTypes = ["image/jpeg", "image/png", "application/pdf"];
        if (!allowedTypes.Contains(contentType.ToLowerInvariant()))
        {
            errors.Add(Error.Validation<Attachment>(
                "Only JPG, PNG and PDF files are allowed."));
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            errors.Add(Error.Validation<Attachment>(
                "File path is required."));
        }

        if (errors.Count != 0)
        {
            return Result<Attachment>.Failure(Error.Validation<Attachment>(
                string.Join("; ", errors.Select(e => e.Message))));
        }

        return Result<Attachment>.Success(new Attachment(
            id, orderId,
            uploadedBy, fileName, filePath, contentType, fileSize,
            description));
    }
}