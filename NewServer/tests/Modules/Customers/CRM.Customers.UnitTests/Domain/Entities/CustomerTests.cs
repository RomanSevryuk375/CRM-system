using CRM.Customers.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Customers.UnitTests.Domain.Entities;

public class CustomerTests
{
    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var userId = new UserId(Guid.NewGuid());
        var name = Name.Create("John").Value;
        var surname = Name.Create("Doe").Value;
        var phone = PhoneNumber.Create("+375291234567").Value;
        var email = Email.Create("john.doe@example.com").Value;

        // Act
        var result = Customer.Create(customerId, userId, name, surname, phone, email);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(customerId);
        result.Value.UserId.Should().Be(userId);
        result.Value.Name.Should().Be(name);
        result.Value.Surname.Should().Be(surname);
        result.Value.PhoneNumber.Should().Be(phone);
        result.Value.Email.Should().Be(email);
    }

    [Fact]
    public void UpdateContactInfo_ChangesPhoneAndEmail()
    {
        // Arrange
        var customer = Customer.Create(
            new CustomerId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()),
            Name.Create("John").Value,
            Name.Create("Doe").Value,
            PhoneNumber.Create("+375291234567").Value,
            Email.Create("old@example.com").Value).Value;

        var newPhone = PhoneNumber.Create("+375297654321").Value;
        var newEmail = Email.Create("new@example.com").Value;

        // Act
        var result = customer.UpdateContactInfo(newPhone, newEmail);

        // Assert
        result.IsSuccess.Should().BeTrue();
        customer.PhoneNumber.Should().Be(newPhone);
        customer.Email.Should().Be(newEmail);
    }
}
