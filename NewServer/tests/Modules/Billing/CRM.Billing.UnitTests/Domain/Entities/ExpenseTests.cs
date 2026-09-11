using CRM.TestHelpers;
using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.Billing.UnitTests.Domain.Entities;

public class ExpenseTests
{
    private static readonly DateOnly Today = new(2026, 9, 11);

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var expenseId = new ExpenseId(Guid.NewGuid());

        // Act
        var result = Expense.Create(
            expenseId,
            Today,
            "Office supplies",
            "Paper and pens",
            ExpenseType.OfficeAndSupplies,
            150m,
            Today);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(expenseId);
        result.Value.Date.Should().Be(Today);
        result.Value.Category.Should().Be("Office supplies");
        result.Value.Description.Should().Be("Paper and pens");
        result.Value.Type.Should().Be(ExpenseType.OfficeAndSupplies);
        result.Value.Amount.Value.Should().Be(150m);
        result.Value.TaxId.Should().BeNull();
        result.Value.ReferenceId.Should().BeNull();
    }

    [Fact]
    public void Create_WithFutureDate_ReturnsFailure()
    {
        // Arrange
        var futureDate = Today.AddDays(1);

        // Act
        var result = Expense.Create(
            new ExpenseId(Guid.NewGuid()),
            futureDate,
            "Rent",
            null,
            ExpenseType.OfficeAndSupplies,
            1000m,
            Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Expense.Errors.FutureDate);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyCategory_ReturnsFailure(string? category)
    {
        // Act
        var result = Expense.Create(
            new ExpenseId(Guid.NewGuid()),
            Today,
            category!,
            null,
            ExpenseType.OfficeAndSupplies,
            100m,
            Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Expense.Errors.CategoryEmpty);
    }

    [Fact]
    public void Create_WithTooLongCategory_ReturnsFailure()
    {
        // Arrange
        var longCategory = new string('x', Expense.MaxCategoryLength + 1);

        // Act
        var result = Expense.Create(
            new ExpenseId(Guid.NewGuid()),
            Today,
            longCategory,
            null,
            ExpenseType.OfficeAndSupplies,
            100m,
            Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Expense.Errors.CategoryTooLong);
    }

    [Fact]
    public void Create_WithTooLongDescription_ReturnsFailure()
    {
        // Arrange
        var longDescription = new string('d', Expense.MaxDescriptionLength + 1);

        // Act
        var result = Expense.Create(
            new ExpenseId(Guid.NewGuid()),
            Today,
            "Category",
            longDescription,
            ExpenseType.OfficeAndSupplies,
            100m,
            Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Expense.Errors.DescriptionTooLong);
    }

    [Fact]
    public void Create_WithZeroAmount_ReturnsFailure()
    {
        // Act
        var result = Expense.Create(
            new ExpenseId(Guid.NewGuid()),
            Today,
            "Category",
            null,
            ExpenseType.OfficeAndSupplies,
            0m,
            Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Expense.Errors.ZeroAmount);
    }

    [Fact]
    public void Create_WithNegativeAmount_ReturnsFailure()
    {
        // Act
        var result = Expense.Create(
            new ExpenseId(Guid.NewGuid()),
            Today,
            "Category",
            null,
            ExpenseType.OfficeAndSupplies,
            -50m,
            Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Money.Errors.NegativeValue);
    }

    [Fact]
    public void UpdateDetails_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var expense = ExpenseMother.CreateDefault(category: "Old Category", description: "Old Desc");

        var yesterday = Today.AddDays(-1);

        // Act
        var result = expense.UpdateDetails(yesterday, "New Category", "New Desc", Today);

        // Assert
        result.IsSuccess.Should().BeTrue();
        expense.Date.Should().Be(yesterday);
        expense.Category.Should().Be("New Category");
        expense.Description.Should().Be("New Desc");
    }

    [Fact]
    public void UpdateDetails_WithFutureDate_ReturnsFailure()
    {
        // Arrange
        var expense = ExpenseMother.CreateDefault(category: "Category", description: null);

        var futureDate = Today.AddDays(2);

        // Act
        var result = expense.UpdateDetails(futureDate, "Category", null, Today);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(Expense.Errors.FutureDate);
    }

    [Fact]
    public void AssignTax_SetsTaxId()
    {
        // Arrange
        var expense = ExpenseMother.CreateDefault(category: "Category", description: null);

        var taxId = new TaxId(Guid.NewGuid());

        // Act
        var result = expense.AssignTax(taxId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        expense.TaxId.Should().Be(taxId);
    }
}

