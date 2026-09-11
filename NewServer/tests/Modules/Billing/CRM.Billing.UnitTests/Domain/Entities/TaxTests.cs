using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Billing.UnitTests.Domain.Entities;

public class TaxTests
{
    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new TaxId(Guid.NewGuid());
        var name = Name.Create("VAT 20%").Value;

        // Act
        var result = Tax.Create(id, name, 20m, TaxType.ValueAddedTax);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Should().Be(name);
        result.Value.Rate.Value.Should().Be(0.20m);
        result.Value.Type.Should().Be(TaxType.ValueAddedTax);
    }

    [Fact]
    public void Create_WithRateExceedingMax_ReturnsFailure()
    {
        // Act
        var result = Tax.Create(
            new TaxId(Guid.NewGuid()),
            Name.Create("VAT").Value,
            150m,
            TaxType.ValueAddedTax);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(TaxRate.Errors.ExceedsMax);
    }

    [Fact]
    public void Create_WithNegativeRate_ReturnsFailure()
    {
        // Act
        var result = Tax.Create(
            new TaxId(Guid.NewGuid()),
            Name.Create("VAT").Value,
            -10m,
            TaxType.ValueAddedTax);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(TaxRate.Errors.NegativeRate);
    }

    [Fact]
    public void UpdateRate_WithValidRate_ReturnsSuccess()
    {
        // Arrange
        var tax = Tax.Create(new TaxId(Guid.NewGuid()), Name.Create("VAT").Value, 10m, TaxType.ValueAddedTax).Value;

        // Act
        var result = tax.UpdateRate(25m);

        // Assert
        result.IsSuccess.Should().BeTrue();
        tax.Rate.Value.Should().Be(0.25m);
    }

    [Fact]
    public void UpdateRate_WithNegativeRate_ReturnsFailure()
    {
        // Arrange
        var tax = Tax.Create(new TaxId(Guid.NewGuid()), Name.Create("VAT").Value, 20m, TaxType.ValueAddedTax).Value;

        // Act
        var result = tax.UpdateRate(-5m);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(TaxRate.Errors.NegativeRate);
    }

    [Fact]
    public void Rename_WithValidName_UpdatesName()
    {
        // Arrange
        var tax = Tax.Create(new TaxId(Guid.NewGuid()), Name.Create("Old Name").Value, 10m, TaxType.ValueAddedTax).Value;
        var newName = Name.Create("New Name").Value;

        // Act
        var result = tax.Rename(newName);

        // Assert
        result.IsSuccess.Should().BeTrue();
        tax.Name.Should().Be(newName);
    }
}
