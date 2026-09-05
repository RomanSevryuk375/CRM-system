// Ignore Spelling: Oem

using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class Part : AggregateRoot<PartId>, IAuditable, ISoftDeletable
{
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
        if (string.IsNullOrWhiteSpace(internalArticle))
        {
            return Result<Part>.Failure(Error.Validation<Part>(Errors.InternalArticleEmpty));
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
    }
}
