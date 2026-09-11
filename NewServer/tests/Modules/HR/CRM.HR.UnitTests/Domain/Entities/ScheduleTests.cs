using CRM.HR.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.HR.UnitTests.Domain.Entities;

public class ScheduleTests
{
    private static readonly DateOnly ScheduleDate = new(2026, 9, 15);

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var scheduleId = new ScheduleId(Guid.NewGuid());
        var workerId = new WorkerId(Guid.NewGuid());
        var shiftId = new ShiftId(Guid.NewGuid());

        // Act
        var result = Schedule.Create(scheduleId, workerId, shiftId, ScheduleDate);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(scheduleId);
        result.Value.WorkerId.Should().Be(workerId);
        result.Value.ShiftId.Should().Be(shiftId);
        result.Value.Date.Should().Be(ScheduleDate);
    }
}
