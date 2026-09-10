// Ignore Spelling: Oem

using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class Part : AggregateRoot<PartId>, IAuditable, ISoftDeletable
{
    public const int MaxArticleLength = 64;
    public const int MaxNameLength = 256;
    public const int MaxManufacturerLength = 128;
    public const int MaxApplicabilityLength = 1000;
    public const int MaxDescriptionLength = 1000;

    private Part(
        PartId id,
        PartCategoryId categoryId,
        string? oemArticle,
        string? manufacturerArticle,
        string internalArtile,
        string? description,
        string name,
        string manufacturer,
        string appllicabillity)
    {
        Id = id;
        CategoryId = categoryId;
        OemArticle = oemArticle;
        ManufacturerArticle = manufacturerArticle;
        InternalArticle = internalArtile;
        Description = description;
        Name = name;
        Manufacturer = manufacturer;
        Applicability = appllicabillity;
    }

#pragma warning disable CS8618
    private Part() { }
#pragma warning restore CS8618

    public PartCategoryId CategoryId { get; private set; }
    public string? OemArticle { get; private set; }
    public string? ManufacturerArticle { get; private set; }
    public string InternalArticle { get; private set; }
    public string? Description { get; private set; }
    public string Name { get; private set; }
    public string Manufacturer { get; private set; }
    public string Applicability { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Part> Create(
        PartId id,
        PartCategoryId categoryId,
        string? oemArticle,
        string? manufacturerArticle,
        string internalArticle,
        string? description,
        string name,
        string manufacturer,
        string applicability)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(internalArticle))
        {
            errors.Add(Error.Validation<Part>(Errors.InternalArticleEmpty));
        }
        else if (internalArticle.Length > MaxArticleLength)
        {
            errors.Add(Error.Validation<Part>(Errors.InternalArticleTooLong));
        }

        if (oemArticle?.Length > MaxArticleLength)
        {
            errors.Add(Error.Validation<Part>(Errors.OemArticleTooLong));
        }

        if (manufacturerArticle?.Length > MaxArticleLength)
        {
            errors.Add(Error.Validation<Part>(Errors.ManufacturerArticleTooLong));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(Error.Validation<Part>(Errors.NameEmpty));
        }
        else if (name.Length > MaxNameLength)
        {
            errors.Add(Error.Validation<Part>(Errors.NameTooLong));
        }

        if (string.IsNullOrWhiteSpace(manufacturer))
        {
            errors.Add(Error.Validation<Part>(Errors.ManufacturerEmpty));
        }
        else if (manufacturer.Length > MaxManufacturerLength)
        {
            errors.Add(Error.Validation<Part>(Errors.ManufacturerTooLong));
        }

        if (string.IsNullOrWhiteSpace(applicability))
        {
            errors.Add(Error.Validation<Part>(Errors.ApplicabilityEmpty));
        }
        else if (applicability.Length > MaxApplicabilityLength)
        {
            errors.Add(Error.Validation<Part>(Errors.ApplicabilityTooLong));
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<Part>(Errors.DescriptionTooLong));
        }

        if (errors.Count != 0)
        {
            return Result<Part>.Failure(Error.Validation<Part>(
                string.Join("; ", errors.Select(e => e.Message))));
        }

        Part part = new(
            id,
            categoryId,
            oemArticle,
            manufacturerArticle,
            internalArticle,
            description,
            name,
            manufacturer,
            applicability);
        part.IncrementVersion();

        return Result<Part>.Success(part);
    }

    public static class Errors
    {
        public const string InternalArticleEmpty = "Internal article cannot be empty.";
        public static readonly string InternalArticleTooLong = $"Internal article exceeds {MaxArticleLength} characters.";
        public static readonly string OemArticleTooLong = $"OEM article exceeds {MaxArticleLength} characters.";
        public static readonly string ManufacturerArticleTooLong = $"Manufacturer article exceeds {MaxArticleLength} characters.";
        public const string NameEmpty = "Name cannot be empty.";
        public static readonly string NameTooLong = $"Name exceeds {MaxNameLength} characters.";
        public const string ManufacturerEmpty = "Manufacturer cannot be empty.";
        public static readonly string ManufacturerTooLong = $"Manufacturer exceeds {MaxManufacturerLength} characters.";
        public const string ApplicabilityEmpty = "Applicability cannot be empty.";
        public static readonly string ApplicabilityTooLong = $"Applicability exceeds {MaxApplicabilityLength} characters.";
        public static readonly string DescriptionTooLong = $"Description exceeds {MaxDescriptionLength} characters.";
    }
}
