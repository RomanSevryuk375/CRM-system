using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.VehicleInspections;

public sealed class VehicleInspection : AggregateRoot<VehicleInspectionId>, ISoftDeletable, IAuditable
{
    public const int MaxTextLength = 2000;

    private readonly List<VehicleInspectionImage> _images = [];

    private VehicleInspection(
        VehicleInspectionId id,
        OrderId orderId,
        WorkerId workerId,
        Mileage mileage,
        FuelLevel fuelLevel,
        VehicleCleanliness cleanlinessLevel,
        bool hasWheelNutKey,
        bool hasServiceBook,
        string? externalDefects,
        string? internalDefects,
        string? personalBelongings,
        string? dashboardWarnings)
    {
        Id = id;
        OrderId = orderId;
        WorkerId = workerId;
        Mileage = mileage;
        FuelLevel = fuelLevel;
        CleanlinessLevel = cleanlinessLevel;
        HasWheelNutKey = hasWheelNutKey;
        HasServiceBook = hasServiceBook;
        ExternalDefects = externalDefects;
        InternalDefects = internalDefects;
        PersonalBelongings = personalBelongings;
        DashboardWarnings = dashboardWarnings;

        Status = InspectionStatus.Draft;
        ClientSign = false;
        WorkerSign = false;
    }

#pragma warning disable CS8618
    private VehicleInspection() { }
#pragma warning restore CS8618

    public OrderId OrderId { get; private set; }
    public WorkerId WorkerId { get; private set; }
    public Mileage Mileage { get; private set; }
    public FuelLevel FuelLevel { get; private set; }

    public VehicleCleanliness CleanlinessLevel { get; private set; }
    public string? PersonalBelongings { get; private set; }
    public string? DashboardWarnings { get; private set; }
    public string? ExternalDefects { get; private set; }
    public string? InternalDefects { get; private set; }

    public bool HasWheelNutKey { get; private set; }
    public bool HasServiceBook { get; private set; }

    public bool ClientSign { get; private set; }
    public bool WorkerSign { get; private set; }
    public InspectionStatus Status { get; private set; }

    public IReadOnlyList<VehicleInspectionImage> Images => _images.AsReadOnly();

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<VehicleInspection> Create(
        VehicleInspectionId id,
        OrderId orderId,
        WorkerId workerId,
        Mileage mileage,
        FuelLevel fuelLevel,
        VehicleCleanliness cleanlinessLevel,
        bool hasWheelNutKey,
        bool hasServiceBook,
        string? externalDefects,
        string? internalDefects,
        string? personalBelongings,
        string? dashboardWarnings)
    {
        List<Error> errors = [];

        if (externalDefects?.Length > MaxTextLength)
        {
            errors.Add(Error.Validation<VehicleInspection>(Errors.ExternalDefectsTooLong));
        }

        if (internalDefects?.Length > MaxTextLength)
        {
            errors.Add(Error.Validation<VehicleInspection>(Errors.InternalDefectsTooLong));
        }

        if (personalBelongings?.Length > MaxTextLength)
        {
            errors.Add(Error.Validation<VehicleInspection>(Errors.PersonalBelongingsTooLong));
        }

        if (dashboardWarnings?.Length > MaxTextLength)
        {
            errors.Add(Error.Validation<VehicleInspection>(Errors.DashboardWarningsTooLong));
        }

        if (errors.Count != 0)
        {
            return Result<VehicleInspection>.Failure(Error.Validation<VehicleInspection>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        VehicleInspection inspection = new(
            id, orderId, workerId,
            mileage, fuelLevel,
            cleanlinessLevel, hasWheelNutKey, hasServiceBook,
            externalDefects, internalDefects, personalBelongings, dashboardWarnings);

        inspection.IncrementVersion();

        return Result<VehicleInspection>.Success(inspection);
    }

    public Result AddImage(VehicleInspectionImageId imageId, string path, string? description)
    {
        if (Status == InspectionStatus.Signed)
        {
            return Result.Failure(Error.Conflict<VehicleInspection>(Errors.CannotAddImageToSigned));
        }

        Result<VehicleInspectionImage> imageResult = VehicleInspectionImage.Create(imageId, Id, path, description);
        if (imageResult.IsFailure)
        {
            return imageResult;
        }

        _images.Add(imageResult.Value);
        IncrementVersion();

        return Result.Success();
    }

    public Result RemoveImage(VehicleInspectionImageId imageId)
    {
        if (Status is InspectionStatus.Signed)
        {
            return Result.Failure(Error.Conflict<VehicleInspection>(Errors.CannotRemoveImageFromSigned));
        }

        VehicleInspectionImage? image = _images.Find(i => i.Id == imageId);
        if (image is null)
        {
            return Result.Success();
        }

        _images.Remove(image);
        IncrementVersion();

        return Result.Success();
    }

    public Result UpdateImageDescription(VehicleInspectionImageId imageId, string? newDescription)
    {
        if (Status == InspectionStatus.Signed)
        {
            return Result.Failure(Error.Conflict<VehicleInspection>(Errors.CannotEditImageInSigned));
        }

        VehicleInspectionImage? image = _images.Find(i => i.Id == imageId);
        if (image is null)
        {
            return Result.Failure(Error.NotFound<VehicleInspectionImage>(Errors.ImageNotFound));
        }

        Result updateResult = image.UpdateDescription(newDescription);
        if (updateResult.IsFailure)
        {
            return updateResult;
        }

        IncrementVersion();

        return Result.Success();
    }

    public Result Sign()
    {
        if (Status is InspectionStatus.Signed)
        {
            return Result.Failure(Error.Conflict<VehicleInspection>(Errors.AlreadySigned));
        }

        ClientSign = true;
        WorkerSign = true;
        Status = InspectionStatus.Signed;

        IncrementVersion();

        return Result.Success();
    }

    public Result ReopenForEditing()
    {
        if (Status is InspectionStatus.Draft)
        {
            return Result.Success();
        }

        Status = InspectionStatus.Draft;
        ClientSign = false;
        WorkerSign = false;

        IncrementVersion();

        return Result.Success();
    }

    public static class Errors
    {
        public static readonly string ExternalDefectsTooLong = $"External defects text exceeds {MaxTextLength} characters.";
        public static readonly string InternalDefectsTooLong = $"Internal defects text exceeds {MaxTextLength} characters.";
        public static readonly string PersonalBelongingsTooLong = $"Personal belongings text exceeds {MaxTextLength} characters.";
        public static readonly string DashboardWarningsTooLong = $"Dashboard warnings text exceeds {MaxTextLength} characters.";
        public const string CannotAddImageToSigned = "Cannot add images to a signed inspection.";
        public const string CannotRemoveImageFromSigned = "Cannot remove images from a signed inspection.";
        public const string CannotEditImageInSigned = "Cannot edit images in a signed inspection.";
        public const string ImageNotFound = "Vehicle inspection image not found.";
        public const string AlreadySigned = "This inspection is already signed.";
    }
}