using CRM.HR.Domain.Entities;
using CRM.HR.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;
using FluentAssertions;
using Xunit;

namespace CRM.HR.UnitTests.Domain.Entities;

public class AbsenceTests
{
    private static readonly WorkerId TestWorkerId = new(Guid.NewGuid());
    private static readonly DateOnly StartDate = new(2026, 9, 1);
    private static readonly DateOnly EndDate = new(2026, 9, 14);

    [Fact]
    public void Create_WithValidDateRange_ReturnsSuccess()
    {
        // Arrange
        var id = new AbsenceId(Guid.NewGuid());

        // Act
        var result = Absence.Create(
            id,
            TestWorkerId,
            AbsenceType.Vacation,
            StartDate,
            EndDate);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(id);
        result.Value.WorkerId.Should().Be(TestWorkerId);
        result.Value.Type.Should().Be(AbsenceType.Vacation);
        result.Value.StartDate.Should().Be(StartDate);
        result.Value.EndDate.Should().Be(EndDate);
    }

    [Fact]
    public void Create_WithValidOpenEndedAbsence_ReturnsSuccess()
    {
        // Arrange
        var id = new AbsenceId(Guid.NewGuid());

        // Act
        var result = Absence.Create(
            id,
            TestWorkerId,
            AbsenceType.SickLeave,
            StartDate,
            endDate: null);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.EndDate.Should().BeNull();
    }

    [Fact]
    public void Create_WhenEndDateBeforeStartDate_ReturnsFailure()
    {
        // Arrange
        var earlierEnd = StartDate.AddDays(-1);

        // Act
        var result = Absence.Create(
            new AbsenceId(Guid.NewGuid()),
            TestWorkerId,
            AbsenceType.PersonalLeave,
            StartDate,
            earlierEnd);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Absence.Errors.InvalidDateRange);
    }
}
