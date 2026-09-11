using CRM.HR.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.TestHelpers;
using FluentAssertions;
using Xunit;

namespace CRM.HR.UnitTests.Domain.Entities;

public class WorkerTests
{
    private static readonly UserId TestUserId = new(Guid.NewGuid());
    private static readonly Name FirstName = Name.Create("Alex").Value;
    private static readonly Name LastName = Name.Create("Petrov").Value;
    private static readonly Money InitialRate = Money.Create(25m).Value;
    private static readonly PhoneNumber Phone = PhoneNumber.Create("+375291234567").Value;
    private static readonly Email EmailAddress = Email.Create("alex@service.com").Value;

    [Fact]
    public void Create_WithValidData_ReturnsSuccess()
    {
        // Arrange
        var workerId = new WorkerId(Guid.NewGuid());

        // Act
        var result = Worker.Create(
            workerId,
            TestUserId,
            FirstName,
            LastName,
            InitialRate,
            Phone,
            EmailAddress);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(workerId);
        result.Value.UserId.Should().Be(TestUserId);
        result.Value.Name.Should().Be(FirstName);
        result.Value.Surname.Should().Be(LastName);
        result.Value.HourlyRate.Should().Be(InitialRate);
        result.Value.PhoneNumber.Should().Be(Phone);
        result.Value.Email.Should().Be(EmailAddress);
        result.Value.Skills.Should().BeEmpty();
    }

    [Fact]
    public void UpdateHourlyRate_ChangesRate()
    {
        // Arrange
        var worker = WorkerMother.CreateDefault();
        var newRate = Money.Create(35m).Value;

        // Act
        var result = worker.UpdateHourlyRate(newRate);

        // Assert
        result.IsSuccess.Should().BeTrue();
        worker.HourlyRate.Should().Be(newRate);
    }

    [Fact]
    public void AddSkill_WithNewSpecialization_AddsSkill()
    {
        // Arrange
        var worker = WorkerMother.CreateDefault();
        var skillId = new SkillId(Guid.NewGuid());
        var specId = new SpecializationId(Guid.NewGuid());

        // Act
        var result = worker.AddSkill(skillId, specId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        worker.Skills.Should().HaveCount(1);
        worker.Skills[0].Id.Should().Be(skillId);
        worker.Skills[0].SpecializationId.Should().Be(specId);
        worker.Skills[0].WorkerId.Should().Be(worker.Id);
    }

    [Fact]
    public void AddSkill_WithDuplicateSpecialization_ReturnsFailure()
    {
        // Arrange
        var worker = WorkerMother.CreateDefault();
        var specId = new SpecializationId(Guid.NewGuid());
        worker.AddSkill(new SkillId(Guid.NewGuid()), specId);

        // Act
        var result = worker.AddSkill(new SkillId(Guid.NewGuid()), specId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Message.Should().Be(Worker.Errors.SpecializationAlreadyExists);
        worker.Skills.Should().HaveCount(1);
    }

    [Fact]
    public void RemoveSkill_WhenExists_RemovesSkill()
    {
        // Arrange
        var worker = WorkerMother.CreateDefault();
        var skillId = new SkillId(Guid.NewGuid());
        worker.AddSkill(skillId, new SpecializationId(Guid.NewGuid()));

        // Act
        var result = worker.RemoveSkill(skillId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        worker.Skills.Should().BeEmpty();
    }

    [Fact]
    public void RemoveSkill_WhenNotFound_ReturnsSuccess()
    {
        // Arrange
        var worker = WorkerMother.CreateDefault();

        // Act
        var result = worker.RemoveSkill(new SkillId(Guid.NewGuid()));

        // Assert
        result.IsSuccess.Should().BeTrue();
        worker.Skills.Should().BeEmpty();
    }
}
