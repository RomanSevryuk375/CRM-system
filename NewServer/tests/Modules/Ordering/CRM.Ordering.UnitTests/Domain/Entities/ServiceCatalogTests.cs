using CRM.TestHelpers;
using CRM.Ordering.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Ordering.UnitTests.Domain.Entities;

public class ServiceCatalogTests
{
    private static readonly StandardHours TestStandardTime = StandardHours.Create(1.5m).Value;

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var jobId = new JobId(Guid.NewGuid());

        // Act
        var result = ServiceCatalogItem.Create(
            jobId,
            "Oil Change",
            "Maintenance",
            "Engine oil and filter change",
            TestStandardTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(jobId);
        result.Value.Title.Should().Be("Oil Change");
        result.Value.Category.Should().Be("Maintenance");
        result.Value.Description.Should().Be("Engine oil and filter change");
        result.Value.StandardTime.Should().Be(TestStandardTime);
    }

    [Fact]
    public void Create_TrimsStrings()
    {
        // Act
        var result = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            "  Oil Change  ",
            "  Maintenance  ",
            "  Some description  ",
            TestStandardTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Title.Should().Be("Oil Change");
        result.Value.Category.Should().Be("Maintenance");
        result.Value.Description.Should().Be("Some description");
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyTitle_ReturnsFailure(string? title)
    {
        // Act
        var result = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            title!,
            "Maintenance",
            null,
            TestStandardTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(ServiceCatalogItem.Errors.TitleEmpty);
    }

    [Fact]
    public void Create_WithTooLongTitle_ReturnsFailure()
    {
        // Arrange
        var longTitle = new string('t', ServiceCatalogItem.MaxTitleLength + 1);

        // Act
        var result = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            longTitle,
            "Maintenance",
            null,
            TestStandardTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(ServiceCatalogItem.Errors.TitleTooLong);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyCategory_ReturnsFailure(string? category)
    {
        // Act
        var result = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            "Oil Change",
            category!,
            null,
            TestStandardTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(ServiceCatalogItem.Errors.CategoryEmpty);
    }

    [Fact]
    public void Create_WithTooLongCategory_ReturnsFailure()
    {
        // Arrange
        var longCategory = new string('c', ServiceCatalogItem.MaxCategoryLength + 1);

        // Act
        var result = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            "Oil Change",
            longCategory,
            null,
            TestStandardTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(ServiceCatalogItem.Errors.CategoryTooLong);
    }

    [Fact]
    public void Create_WithTooLongDescription_ReturnsFailure()
    {
        // Arrange
        var longDesc = new string('d', ServiceCatalogItem.MaxDescriptionLength + 1);

        // Act
        var result = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            "Oil Change",
            "Maintenance",
            longDesc,
            TestStandardTime);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(ServiceCatalogItem.Errors.DescriptionTooLong);
    }

    [Fact]
    public void UpdateDetails_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var item = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            "Old Title",
            "Old Category",
            null,
            TestStandardTime).Value;

        // Act
        var result = item.UpdateDetails("New Title", "New Category", "New Desc");

        // Assert
        result.IsSuccess.Should().BeTrue();
        item.Title.Should().Be("New Title");
        item.Category.Should().Be("New Category");
        item.Description.Should().Be("New Desc");
    }

    [Fact]
    public void UpdateStandardTime_UpdatesTime()
    {
        // Arrange
        var item = ServiceCatalogItem.Create(
            new JobId(Guid.NewGuid()),
            "Oil Change",
            "Maintenance",
            null,
            TestStandardTime).Value;

        var newTime = StandardHours.Create(2.0m).Value;

        // Act
        var result = item.UpdateStandardTime(newTime);

        // Assert
        result.IsSuccess.Should().BeTrue();
        item.StandardTime.Should().Be(newTime);
    }
}

