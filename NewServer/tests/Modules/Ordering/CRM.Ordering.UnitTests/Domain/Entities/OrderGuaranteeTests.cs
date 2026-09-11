using CRM.TestHelpers;
using CRM.Ordering.Domain.Entities.Orders;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.Ordering.UnitTests.Domain.Entities;

public class OrderGuaranteeTests
{
    private static readonly OrderId TestOrderId = new(Guid.NewGuid());
    private static readonly DateOnly StartDate = new(2026, 9, 1);
    private static readonly DateOnly EndDate = new(2027, 9, 1);

    [Fact]
    public void Create_WithOnlyPart_ReturnsSuccess()
    {
        // Arrange
        var guaranteeId = new OrderGuaranteeId(Guid.NewGuid());
        var partId = new OrderPartId(Guid.NewGuid());

        // Act
        var result = OrderGuarantee.Create(
            guaranteeId,
            TestOrderId,
            orderPartId: partId,
            orderWorkId: null,
            StartDate,
            EndDate,
            "Part warranty",
            "1 year warranty for brake pads");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(guaranteeId);
        result.Value.OrderId.Should().Be(TestOrderId);
        result.Value.OrderPartId.Should().Be(partId);
        result.Value.OrderWorkId.Should().BeNull();
        result.Value.DateStart.Should().Be(StartDate);
        result.Value.DateEnd.Should().Be(EndDate);
        result.Value.Description.Should().Be("Part warranty");
        result.Value.Terms.Should().Be("1 year warranty for brake pads");
    }

    [Fact]
    public void Create_WithOnlyWork_ReturnsSuccess()
    {
        // Arrange
        var guaranteeId = new OrderGuaranteeId(Guid.NewGuid());
        var workId = new OrderWorkId(Guid.NewGuid());

        // Act
        var result = OrderGuarantee.Create(
            guaranteeId,
            TestOrderId,
            orderPartId: null,
            orderWorkId: workId,
            StartDate,
            EndDate,
            null,
            "Labor guarantee");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.OrderPartId.Should().BeNull();
        result.Value.OrderWorkId.Should().Be(workId);
    }

    [Fact]
    public void Create_WithBothPartAndWorkSpecified_ReturnsFailure()
    {
        // Act
        var result = OrderGuarantee.Create(
            new OrderGuaranteeId(Guid.NewGuid()),
            TestOrderId,
            orderPartId: new OrderPartId(Guid.NewGuid()),
            orderWorkId: new OrderWorkId(Guid.NewGuid()),
            StartDate,
            EndDate,
            null,
            "Terms");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderGuarantee.Errors.BothPartAndWorkSpecified);
    }

    [Theory]
    [InlineData(2026, 9, 1, 2026, 9, 1)] // equal
    [InlineData(2026, 9, 2, 2026, 9, 1)] // start > end
    public void Create_WhenStartDateAfterOrEqualEndDate_ReturnsFailure(int y1, int m1, int d1, int y2, int m2, int d2)
    {
        // Act
        var result = OrderGuarantee.Create(
            new OrderGuaranteeId(Guid.NewGuid()),
            TestOrderId,
            orderPartId: new OrderPartId(Guid.NewGuid()),
            orderWorkId: null,
            new DateOnly(y1, m1, d1),
            new DateOnly(y2, m2, d2),
            null,
            "Terms");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderGuarantee.Errors.InvalidDateRange);
    }

    [Theory]
    [MemberData(nameof(EmptyStringData.Values), MemberType = typeof(EmptyStringData))]
    public void Create_WithEmptyTerms_ReturnsFailure(string? terms)
    {
        // Act
        var result = OrderGuarantee.Create(
            new OrderGuaranteeId(Guid.NewGuid()),
            TestOrderId,
            orderPartId: new OrderPartId(Guid.NewGuid()),
            orderWorkId: null,
            StartDate,
            EndDate,
            null,
            terms!);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderGuarantee.Errors.TermsEmpty);
    }

    [Fact]
    public void Create_WithTooLongTerms_ReturnsFailure()
    {
        // Arrange
        var longTerms = new string('t', OrderGuarantee.MaxTermsLength + 1);

        // Act
        var result = OrderGuarantee.Create(
            new OrderGuaranteeId(Guid.NewGuid()),
            TestOrderId,
            orderPartId: new OrderPartId(Guid.NewGuid()),
            orderWorkId: null,
            StartDate,
            EndDate,
            null,
            longTerms);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderGuarantee.Errors.TermsTooLong);
    }

    [Fact]
    public void Create_WithTooLongDescription_ReturnsFailure()
    {
        // Arrange
        var longDesc = new string('d', OrderGuarantee.MaxDescriptionLength + 1);

        // Act
        var result = OrderGuarantee.Create(
            new OrderGuaranteeId(Guid.NewGuid()),
            TestOrderId,
            orderPartId: new OrderPartId(Guid.NewGuid()),
            orderWorkId: null,
            StartDate,
            EndDate,
            longDesc,
            "Valid terms");

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Contain(OrderGuarantee.Errors.DescriptionTooLong);
    }
}

