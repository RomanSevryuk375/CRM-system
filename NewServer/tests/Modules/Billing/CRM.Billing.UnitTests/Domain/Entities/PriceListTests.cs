using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.TestHelpers;
using FluentAssertions;
using Xunit;

namespace CRM.Billing.UnitTests.Domain.Entities;

public class PriceListTests
{
    private static readonly DateOnly ValidFrom = new(2026, 1, 1);

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new PriceListId(Guid.NewGuid());
        var name = Name.Create("Standard Price List").Value;

        // Act
        var result = PriceList.Create(id, name, ValidFrom, 50m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Should().Be(name);
        result.Value.ValidFrom.Should().Be(ValidFrom);
        result.Value.ValidTo.Should().BeNull();
        result.Value.IsDefault.Should().BeFalse();
        result.Value.BaseHourlyRate.Value.Should().Be(50m);
        result.Value.Items.Should().BeEmpty();
    }

    [Fact]
    public void Create_WithNegativeBaseHourlyRate_ReturnsFailure()
    {
        // Act
        var result = PriceList.Create(
            new PriceListId(Guid.NewGuid()),
            Name.Create("Standard").Value,
            ValidFrom,
            -10m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Money.Errors.NegativeValue);
    }

    [Fact]
    public void SetFixedPriceForJob_NewJob_AddsItem()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);
        var itemId = new PriceListItemId(Guid.NewGuid());
        var jobId = new JobId(Guid.NewGuid());

        // Act
        var result = priceList.SetFixedPriceForJob(itemId, jobId, 120m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        priceList.Items.Should().HaveCount(1);
        priceList.Items[0].Id.Should().Be(itemId);
        priceList.Items[0].JobId.Should().Be(jobId);
        priceList.Items[0].FixedPrice.Value.Should().Be(120m);
    }

    [Fact]
    public void SetFixedPriceForJob_ExistingJob_UpdatesPrice()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);
        var jobId = new JobId(Guid.NewGuid());
        priceList.SetFixedPriceForJob(new PriceListItemId(Guid.NewGuid()), jobId, 100m);

        // Act
        var result = priceList.SetFixedPriceForJob(new PriceListItemId(Guid.NewGuid()), jobId, 150m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        priceList.Items.Should().HaveCount(1);
        priceList.Items[0].FixedPrice.Value.Should().Be(150m);
    }

    [Fact]
    public void SetFixedPriceForJob_WithNegativePrice_ReturnsFailure()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);

        // Act
        var result = priceList.SetFixedPriceForJob(new PriceListItemId(Guid.NewGuid()), new JobId(Guid.NewGuid()), -20m);

        // Assert
        result.IsFailure.Should().BeTrue();
        priceList.Items.Should().BeEmpty();
    }

    [Fact]
    public void Deactivate_WithValidDate_SetsValidToAndRemovesDefault()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);
        priceList.MakeDefault();
        priceList.IsDefault.Should().BeTrue();
        var validTo = new DateOnly(2026, 12, 31);

        // Act
        var result = priceList.Deactivate(validTo);

        // Assert
        result.IsSuccess.Should().BeTrue();
        priceList.ValidTo.Should().Be(validTo);
        priceList.IsDefault.Should().BeFalse();
    }

    [Fact]
    public void Deactivate_WithDateBeforeValidFrom_ReturnsFailure()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);
        var earlierDate = ValidFrom.AddDays(-1);

        // Act
        var result = priceList.Deactivate(earlierDate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(PriceList.Errors.InvalidDeactivationDate);
    }

    [Fact]
    public void Deactivate_WhenAlreadyDeactivatedEarlier_ReturnsFailure()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);
        priceList.Deactivate(new DateOnly(2026, 6, 30));

        // Act: trying to deactivate with date later than existing ValidTo
        var result = priceList.Deactivate(new DateOnly(2026, 12, 31));

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(PriceList.Errors.AlreadyDeactivatedEarlier);
    }

    [Fact]
    public void IsValidOn_BeforeValidFrom_ReturnsFalse()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);

        // Act & Assert
        priceList.IsValidOn(ValidFrom.AddDays(-1)).Should().BeFalse();
    }

    [Fact]
    public void IsValidOn_AfterValidTo_ReturnsFalse()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);
        priceList.Deactivate(new DateOnly(2026, 6, 30));

        // Act & Assert
        priceList.IsValidOn(new DateOnly(2026, 7, 1)).Should().BeFalse();
    }

    [Fact]
    public void IsValidOn_WithinRange_ReturnsTrue()
    {
        // Arrange
        var priceList = PriceListMother.CreateDefault(validFrom: ValidFrom);
        priceList.Deactivate(new DateOnly(2026, 6, 30));

        // Act & Assert
        priceList.IsValidOn(new DateOnly(2026, 3, 15)).Should().BeTrue();
        priceList.IsValidOn(ValidFrom).Should().BeTrue();
        priceList.IsValidOn(new DateOnly(2026, 6, 30)).Should().BeTrue();
    }
}
