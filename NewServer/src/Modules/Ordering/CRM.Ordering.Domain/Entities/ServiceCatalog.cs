using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities;

public sealed class ServiceCatalogItem : AggregateRoot<JobId>, ISoftDeletable, IAuditable, IHasVersion
{
    private const int MaxTitleLength = 128;
    private const int MaxCategoryLength = 128;
    private const int MaxDescriptionLength = 2000;

    private ServiceCatalogItem(
        JobId id,
        string title,
        string category,
        string? description,
        StandardHours standardTime)
    {
        Id = id;
        Title = title;
        Category = category;
        Description = description;
        StandardTime = standardTime;
    }

#pragma warning disable CS8618
    private ServiceCatalogItem() { }
#pragma warning restore CS8618

    public string Title { get; private set; }
    public string Category { get; private set; }
    public string? Description { get; private set; }
    public StandardHours StandardTime { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }

    public Guid Version { get; private set; }
#pragma warning restore S1144

    public static Result<ServiceCatalogItem> Create(
        JobId id,
        string title,
        string category,
        string? description,
        decimal standardTime)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                "Title cannot be empty."));
        }
        else if (title.Length > MaxTitleLength)
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                $"Title exceeds {MaxTitleLength} characters."));
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                "Category cannot be empty."));
        }
        else if (category.Length > MaxCategoryLength)
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                $"Category exceeds {MaxCategoryLength} characters."));
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                $"Description exceeds {MaxDescriptionLength} characters."));
        }

        Result<StandardHours> timeResult = StandardHours.Create(standardTime);
        if (timeResult.IsFailure)
        {
            errors.Add(timeResult.Error);
        }

        if (errors.Count != 0)
        {
            return Result<ServiceCatalogItem>.Failure(Error.Validation<ServiceCatalogItem>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        ServiceCatalogItem catalogItem = new(
            id,
            title.Trim(),
            category.Trim(),
            description?.Trim(),
            timeResult.Value);

        catalogItem.IncrementVersion();

        return Result<ServiceCatalogItem>.Success(catalogItem);
    }

    public Result UpdateDetails(string title, string category, string? description)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(title))
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                "Title cannot be empty."));
        }
        else if (title.Length > MaxTitleLength)
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                $"Title exceeds {MaxTitleLength} characters."));
        }

        if (string.IsNullOrWhiteSpace(category))
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                "Category cannot be empty."));
        }
        else if (category.Length > MaxCategoryLength)
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                $"Category exceeds {MaxCategoryLength} characters."));
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<ServiceCatalogItem>(
                $"Description exceeds {MaxDescriptionLength} characters."));
        }

        if (errors.Count != 0)
        {
            return Result.Failure(Error.Validation<ServiceCatalogItem>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        Title = title.Trim();
        Category = category.Trim();
        Description = description?.Trim();

        IncrementVersion();

        return Result.Success();
    }

    public Result UpdateStandardTime(decimal newStandardTime)
    {
        Result<StandardHours> timeResult = StandardHours.Create(newStandardTime);
        if (timeResult.IsFailure)
        {
            return Result.Failure(timeResult.Error);
        }

        StandardTime = timeResult.Value;

        IncrementVersion();

        return Result.Success();
    }

    private void IncrementVersion()
    {
        Version = Guid.NewGuid();
    }
}