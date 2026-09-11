using CRM.TestHelpers;
using CRM.Ordering.Domain.Entities.VehicleInspections;
using CRM.Ordering.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Ordering.UnitTests.Domain.Entities;

public class VehicleInspectionTests
{
    private static readonly OrderId TestOrderId = new(Guid.NewGuid());
    private static readonly WorkerId TestWorkerId = new(Guid.NewGuid());
    private static readonly Mileage TestMileage = Mileage.Create(60000).Value;
    private static readonly FuelLevel TestFuelLevel = FuelLevel.Create(75).Value;

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new VehicleInspectionId(Guid.NewGuid());

        // Act
        var result = VehicleInspection.Create(
            id,
            TestOrderId,
            TestWorkerId,
            TestMileage,
            TestFuelLevel,
            VehicleCleanliness.Clean,
            hasWheelNutKey: true,
            hasServiceBook: true,
            externalDefects: "Scratch on bumper",
            internalDefects: null,
            personalBelongings: "Umbrella in trunk",
            dashboardWarnings: "Check engine");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.OrderId.Should().Be(TestOrderId);
        result.Value.WorkerId.Should().Be(TestWorkerId);
        result.Value.Mileage.Should().Be(TestMileage);
        result.Value.FuelLevel.Should().Be(TestFuelLevel);
        result.Value.CleanlinessLevel.Should().Be(VehicleCleanliness.Clean);
        result.Value.HasWheelNutKey.Should().BeTrue();
        result.Value.HasServiceBook.Should().BeTrue();
        result.Value.Status.Should().Be(InspectionStatus.Draft);
        result.Value.ClientSign.Should().BeFalse();
        result.Value.WorkerSign.Should().BeFalse();
        result.Value.Images.Should().BeEmpty();
    }

    [Fact]
    public void Create_WithTooLongExternalDefects_ReturnsFailure()
    {
        // Arrange
        var longText = new string('x', VehicleInspection.MaxTextLength + 1);

        // Act
        var result = VehicleInspection.Create(
            new VehicleInspectionId(Guid.NewGuid()),
            TestOrderId,
            TestWorkerId,
            TestMileage,
            TestFuelLevel,
            VehicleCleanliness.Clean,
            true, true,
            longText, null, null, null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(VehicleInspection.Errors.ExternalDefectsTooLong);
    }

    [Fact]
    public void AddImage_WhenDraft_AddsImage()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        var imageId = new VehicleInspectionImageId(Guid.NewGuid());

        // Act
        var result = inspection.AddImage(imageId, "/images/bumper.jpg", "Bumper scratch");

        // Assert
        result.IsSuccess.Should().BeTrue();
        inspection.Images.Should().HaveCount(1);
        inspection.Images[0].Id.Should().Be(imageId);
        inspection.Images[0].Path.Should().Be("/images/bumper.jpg");
        inspection.Images[0].Description.Should().Be("Bumper scratch");
    }

    [Fact]
    public void AddImage_WhenSigned_ReturnsFailure()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        inspection.Sign();

        // Act
        var result = inspection.AddImage(new VehicleInspectionImageId(Guid.NewGuid()), "/img.jpg", null);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(VehicleInspection.Errors.CannotAddImageToSigned);
    }

    [Fact]
    public void RemoveImage_WhenDraft_RemovesImage()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        var imageId = new VehicleInspectionImageId(Guid.NewGuid());
        inspection.AddImage(imageId, "/img.jpg", null);

        // Act
        var result = inspection.RemoveImage(imageId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        inspection.Images.Should().BeEmpty();
    }

    [Fact]
    public void Sign_WhenDraft_SignsInspection()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        // Act
        var result = inspection.Sign();

        // Assert
        result.IsSuccess.Should().BeTrue();
        inspection.Status.Should().Be(InspectionStatus.Signed);
        inspection.ClientSign.Should().BeTrue();
        inspection.WorkerSign.Should().BeTrue();
    }

    [Fact]
    public void Sign_WhenAlreadySigned_ReturnsFailure()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        inspection.Sign();

        // Act
        var result = inspection.Sign();

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(VehicleInspection.Errors.AlreadySigned);
    }

    [Fact]
    public void ReopenForEditing_WhenSigned_ReopensAsDraft()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        inspection.Sign();

        // Act
        var result = inspection.ReopenForEditing();

        // Assert
        result.IsSuccess.Should().BeTrue();
        inspection.Status.Should().Be(InspectionStatus.Draft);
        inspection.ClientSign.Should().BeFalse();
        inspection.WorkerSign.Should().BeFalse();
    }

    [Fact]
    public void ReopenForEditing_WhenDraft_ReturnsSuccess()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        // Act
        var result = inspection.ReopenForEditing();

        // Assert
        result.IsSuccess.Should().BeTrue();
        inspection.Status.Should().Be(InspectionStatus.Draft);
    }

    [Fact]
    public void RemoveImage_WhenSigned_ReturnsFailure()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();
        var imageId = new VehicleInspectionImageId(Guid.NewGuid());
        inspection.AddImage(imageId, "/img.jpg", null);
        inspection.Sign();

        // Act
        var result = inspection.RemoveImage(imageId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(VehicleInspection.Errors.CannotRemoveImageFromSigned);
        inspection.Images.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveImage_WhenNotFound_ReturnsSuccess()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        // Act
        var result = inspection.RemoveImage(new VehicleInspectionImageId(Guid.NewGuid()));

        // Assert
        result.IsSuccess.Should().BeTrue();
        inspection.Images.Should().BeEmpty();
    }

    [Fact]
    public void UpdateImageDescription_WhenDraft_UpdatesDescription()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();
        var imageId = new VehicleInspectionImageId(Guid.NewGuid());
        inspection.AddImage(imageId, "/img.jpg", "Old description");

        // Act
        var result = inspection.UpdateImageDescription(imageId, "New description");

        // Assert
        result.IsSuccess.Should().BeTrue();
        inspection.Images[0].Description.Should().Be("New description");
    }

    [Fact]
    public void UpdateImageDescription_WhenSigned_ReturnsFailure()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();
        var imageId = new VehicleInspectionImageId(Guid.NewGuid());
        inspection.AddImage(imageId, "/img.jpg", "Old description");
        inspection.Sign();

        // Act
        var result = inspection.UpdateImageDescription(imageId, "New description");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(VehicleInspection.Errors.CannotEditImageInSigned);
    }

    [Fact]
    public void UpdateImageDescription_WhenImageNotFound_ReturnsFailure()
    {
        // Arrange
        var inspection = VehicleInspectionMother.CreateDraft();

        // Act
        var result = inspection.UpdateImageDescription(new VehicleInspectionImageId(Guid.NewGuid()), "New description");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(VehicleInspection.Errors.ImageNotFound);
    }
}


