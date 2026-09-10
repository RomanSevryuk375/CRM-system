using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities;

public sealed class Attachment : AggregateRoot<AttachmentId>, ISoftDeletable, IAuditable
{
    public const int MaxFileSize = 15 * 1024 * 1024;
    public const int MaxFileNameLength = 256;
    public const int MaxFilePathLength = 1024;
    public const int MaxContentTypeLength = 128;
    public const int MaxDescriptionLength = 2000;

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
            errors.Add(Error.Validation<Attachment>(Errors.InvalidFileSize));
        }

        if (string.IsNullOrWhiteSpace(fileName))
        {
            errors.Add(Error.Validation<Attachment>(Errors.FileNameRequired));
        }
        else if (fileName.Length > MaxFileNameLength)
        {
            errors.Add(Error.Validation<Attachment>(Errors.FileNameTooLong));
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            errors.Add(Error.Validation<Attachment>(Errors.FilePathRequired));
        }
        else if (filePath.Length > MaxFilePathLength)
        {
            errors.Add(Error.Validation<Attachment>(Errors.FilePathTooLong));
        }

        if (string.IsNullOrWhiteSpace(contentType))
        {
            errors.Add(Error.Validation<Attachment>(Errors.ContentTypeRequired));
        }
        else if (contentType.Length > MaxContentTypeLength)
        {
            errors.Add(Error.Validation<Attachment>(Errors.ContentTypeTooLong));
        }
        else
        {
            string[] allowedTypes = ["image/jpeg", "image/png", "application/pdf"];
            if (!allowedTypes.Contains(contentType.ToLowerInvariant()))
            {
                errors.Add(Error.Validation<Attachment>(Errors.UnsupportedContentType));
            }
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<Attachment>(Errors.DescriptionTooLong));
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

    public static class Errors
    {
        public static readonly string InvalidFileSize = $"File size must be between 1 byte and {MaxFileSize / (1024 * 1024)} MB.";
        public const string FileNameRequired = "File name is required.";
        public static readonly string FileNameTooLong = $"File name exceeds {MaxFileNameLength} characters.";
        public const string FilePathRequired = "File path is required.";
        public static readonly string FilePathTooLong = $"File path exceeds {MaxFilePathLength} characters.";
        public const string ContentTypeRequired = "Content type is required.";
        public static readonly string ContentTypeTooLong = $"Content type exceeds {MaxContentTypeLength} characters.";
        public const string UnsupportedContentType = "Only JPG, PNG and PDF files are allowed.";
        public static readonly string DescriptionTooLong = $"Description exceeds {MaxDescriptionLength} characters.";
    }
}