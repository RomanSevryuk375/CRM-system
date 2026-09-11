using CRM.TestHelpers;
using CRM.Inventory.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.Inventory.UnitTests.Domain.Entities;

public class PartTests
{
    private static readonly PartCategoryId CategoryId = new(Guid.NewGuid());
    private const string ValidInternalArticle = "INT-12345";
    private const string ValidName = "Brake Disc";
    private const string ValidManufacturer = "Brembo";
    private const string ValidApplicability = "BMW E46, E39";

    [Fact]
    public void Create_WithAllValidFields_ReturnsSuccess()
    {
        // Arrange
        var partId = new PartId(Guid.NewGuid());

        // Act
        var result = Part.Create(
            partId,
            CategoryId,
            "OEM-998877",
            "MF-554433",
            ValidInternalArticle,
            "Ventilated brake disc",
            ValidName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(partId);
        result.Value.CategoryId.Should().Be(CategoryId);
        result.Value.OemArticle.Should().Be("OEM-998877");
        result.Value.ManufacturerArticle.Should().Be("MF-554433");
        result.Value.InternalArticle.Should().Be(ValidInternalArticle);
        result.Value.Description.Should().Be("Ventilated brake disc");
        result.Value.Name.Should().Be(ValidName);
        result.Value.Manufacturer.Should().Be(ValidManufacturer);
        result.Value.Applicability.Should().Be(ValidApplicability);
    }

    [Fact]
    public void Create_WithNullOptionalFields_ReturnsSuccess()
    {
        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            oemArticle: null,
            manufacturerArticle: null,
            ValidInternalArticle,
            description: null,
            ValidName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.OemArticle.Should().BeNull();
        result.Value.ManufacturerArticle.Should().BeNull();
        result.Value.Description.Should().BeNull();
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyInternalArticle_ReturnsFailure(string? article)
    {
        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            article!,
            null,
            ValidName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.InternalArticleEmpty);
    }

    [Fact]
    public void Create_WithTooLongInternalArticle_ReturnsFailure()
    {
        // Arrange
        var longArticle = new string('x', Part.MaxArticleLength + 1);

        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            longArticle,
            null,
            ValidName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.InternalArticleTooLong);
    }

    [Fact]
    public void Create_WithTooLongOemArticle_ReturnsFailure()
    {
        // Arrange
        var longOem = new string('o', Part.MaxArticleLength + 1);

        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            longOem,
            null,
            ValidInternalArticle,
            null,
            ValidName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.OemArticleTooLong);
    }

    [Fact]
    public void Create_WithTooLongManufacturerArticle_ReturnsFailure()
    {
        // Arrange
        var longMf = new string('m', Part.MaxArticleLength + 1);

        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            longMf,
            ValidInternalArticle,
            null,
            ValidName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.ManufacturerArticleTooLong);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyName_ReturnsFailure(string? name)
    {
        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            ValidInternalArticle,
            null,
            name!,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.NameEmpty);
    }

    [Fact]
    public void Create_WithTooLongName_ReturnsFailure()
    {
        // Arrange
        var longName = new string('n', Part.MaxNameLength + 1);

        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            ValidInternalArticle,
            null,
            longName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.NameTooLong);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyManufacturer_ReturnsFailure(string? manufacturer)
    {
        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            ValidInternalArticle,
            null,
            ValidName,
            manufacturer!,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.ManufacturerEmpty);
    }

    [Fact]
    public void Create_WithTooLongManufacturer_ReturnsFailure()
    {
        // Arrange
        var longMf = new string('m', Part.MaxManufacturerLength + 1);

        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            ValidInternalArticle,
            null,
            ValidName,
            longMf,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.ManufacturerTooLong);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyApplicability_ReturnsFailure(string? applicability)
    {
        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            ValidInternalArticle,
            null,
            ValidName,
            ValidManufacturer,
            applicability!);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.ApplicabilityEmpty);
    }

    [Fact]
    public void Create_WithTooLongApplicability_ReturnsFailure()
    {
        // Arrange
        var longApp = new string('a', Part.MaxApplicabilityLength + 1);

        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            ValidInternalArticle,
            null,
            ValidName,
            ValidManufacturer,
            longApp);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.ApplicabilityTooLong);
    }

    [Fact]
    public void Create_WithTooLongDescription_ReturnsFailure()
    {
        // Arrange
        var longDesc = new string('d', Part.MaxDescriptionLength + 1);

        // Act
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            ValidInternalArticle,
            longDesc,
            ValidName,
            ValidManufacturer,
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.DescriptionTooLong);
    }

    [Fact]
    public void Create_AccumulatesAllErrors()
    {
        // Act: Empty internal article + Empty name + Empty manufacturer
        var result = Part.Create(
            new PartId(Guid.NewGuid()),
            CategoryId,
            null,
            null,
            "",
            null,
            "",
            "",
            ValidApplicability);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Part.Errors.InternalArticleEmpty);
        result.Error.Message.Should().Contain(Part.Errors.NameEmpty);
        result.Error.Message.Should().Contain(Part.Errors.ManufacturerEmpty);
    }
}

