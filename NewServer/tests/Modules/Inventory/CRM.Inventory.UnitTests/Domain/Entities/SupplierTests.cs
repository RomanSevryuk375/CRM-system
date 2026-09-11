using CRM.TestHelpers;
using CRM.Inventory.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Inventory.UnitTests.Domain.Entities;

public class SupplierTests
{
    private static readonly Name ValidName = Name.Create("AutoParts LLC").Value;

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var id = new SupplierId(Guid.NewGuid());

        // Act
        var result = Supplier.Create(id, ValidName, "+375291234567, info@autoparts.com");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.Name.Should().Be(ValidName);
        result.Value.Contacts.Should().Be("+375291234567, info@autoparts.com");
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyContacts_ReturnsFailure(string? contacts)
    {
        // Act
        var result = Supplier.Create(new SupplierId(Guid.NewGuid()), ValidName, contacts!);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Supplier.Errors.ContactsEmpty);
    }

    [Fact]
    public void Create_WithTooLongContacts_ReturnsFailure()
    {
        // Arrange
        var longContacts = new string('c', Supplier.MaxContactsLength + 1);

        // Act
        var result = Supplier.Create(new SupplierId(Guid.NewGuid()), ValidName, longContacts);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Supplier.Errors.ContactsTooLong);
    }
}

