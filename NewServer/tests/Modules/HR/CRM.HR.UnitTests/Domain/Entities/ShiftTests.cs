using CRM.HR.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using FluentAssertions;
using Xunit;

namespace CRM.HR.UnitTests.Domain.Entities;

public class ShiftTests
{
    private static readonly Name ShiftName = Name.Create("Morning Shift").Value;

    [Fact]
    public void Create_WithValidTimeRange_ReturnsSuccess()
    {
        // Arrange
        var shiftId = new ShiftId(Guid.NewGuid());
        var start = new TimeOnly(8, 0);
        var end = new TimeOnly(17, 0);

        // Act
        var result = Shift.Create(shiftId, ShiftName, start, end);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(shiftId);
        result.Value.Name.Should().Be(ShiftName);
        result.Value.StartAt.Should().Be(start);
        result.Value.EndAt.Should().Be(end);
    }

    [Theory]
    [InlineData(17, 0, 8, 0)]  // start > end
    [InlineData(9, 0, 9, 0)]   // start == end
    public void Create_WhenStartAtAfterOrEqualEndAt_ReturnsFailure(int h1, int m1, int h2, int m2)
    {
        // Arrange
        var shiftId = new ShiftId(Guid.NewGuid());
        var start = new TimeOnly(h1, m1);
        var end = new TimeOnly(h2, m2);

        // Act
        var result = Shift.Create(shiftId, ShiftName, start, end);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Shift.Errors.InvalidTimeRange);
    }
}
