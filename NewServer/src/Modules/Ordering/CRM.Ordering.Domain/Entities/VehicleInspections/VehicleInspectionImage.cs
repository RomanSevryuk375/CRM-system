using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.VehicleInspections;

public sealed class VehicleInspectionImage : IEntity<VehicleInspectionImageId>
{
    private const int MaxPathLength = 256;
    private const int MaxDescriptionLength = 2000;

    internal VehicleInspectionImage(
        VehicleInspectionImageId id,
        VehicleInspectionId inspectionId,
        string path,
        string? description)
    {
        Id = id;
        InspectionId = inspectionId;
        Path = path;
        Description = description;
    }

#pragma warning disable CS8618
    private VehicleInspectionImage() { }
#pragma warning restore CS8618

    public VehicleInspectionImageId Id { get; private set; }
    public VehicleInspectionId InspectionId { get; private set; }
    public string Path { get; private set; }
    public string? Description { get; private set; }

    internal static Result<VehicleInspectionImage> Create(
        VehicleInspectionImageId id,
        VehicleInspectionId inspectionId,
        string path,
        string? description)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(path))
        {
            errors.Add(Error.Validation<VehicleInspectionImage>(Errors.PathEmpty));
        }
        else if (path.Length > MaxPathLength)
        {
            errors.Add(Error.Validation<VehicleInspectionImage>(Errors.PathTooLong));
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<VehicleInspectionImage>(Errors.DescriptionTooLong));
        }

        if (errors.Count != 0)
        {
            return Result<VehicleInspectionImage>.Failure(Error.Validation<VehicleInspectionImage>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        return Result<VehicleInspectionImage>.Success(new VehicleInspectionImage(
            id, inspectionId, path, description));
    }

    internal Result UpdateDescription(string? newDescription)
    {
        if (newDescription?.Length > MaxDescriptionLength)
        {
            return Result.Failure(Error.Validation<VehicleInspectionImage>(Errors.DescriptionTooLong));
        }

        Description = newDescription;

        return Result.Success();
    }

    public static class Errors
    {
        public const string PathEmpty = "Image path cannot be empty.";
        public static readonly string PathTooLong = $"Image path exceeds {MaxPathLength} characters.";
        public static readonly string DescriptionTooLong = $"Image description exceeds {MaxDescriptionLength} characters.";
    }
}