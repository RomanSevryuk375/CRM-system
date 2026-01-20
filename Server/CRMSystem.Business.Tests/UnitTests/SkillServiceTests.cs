using CRMSystem.Business.Abstractions;
using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.ProjectionModels.Skill;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class SkillServiceTests
{
    private readonly Mock<ISkillRepository> _skillRepoMock;
    private readonly Mock<ISpecializationRepository> _specRepoMock;
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<IUserContext> _userContextMock;
    private readonly Mock<ILogger<SkillService>> _loggerMock;
    private readonly SkillService _service;

    public SkillServiceTests()
    {
        _skillRepoMock = new Mock<ISkillRepository>();
        _specRepoMock = new Mock<ISpecializationRepository>();
        _workerRepoMock = new Mock<IWorkerRepository>();
        _userContextMock = new Mock<IUserContext>();
        _loggerMock = new Mock<ILogger<SkillService>>();

        _service = new SkillService(
            _skillRepoMock.Object,
            _specRepoMock.Object,
            _workerRepoMock.Object,
            _userContextMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateSkill_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var skill = ValidObjects.CreateValidSkill();

        _workerRepoMock.Setup(x => x.Exists(
            skill.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSkill(skill, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _skillRepoMock.Verify(x => x.Create(
            It.IsAny<Skill>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateSkill_ShouldThrowNotFoundException_WhenSpecializationDoesNotExist()
    {
        var skill = ValidObjects.CreateValidSkill();

        _workerRepoMock.Setup(x => x.Exists(
            skill.WorkerId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _specRepoMock.Setup(x => x.Exists(
            skill.SpecializationId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateSkill(skill, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _skillRepoMock.Verify(x => x.Create(
            It.IsAny<Skill>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task UpdateSkill_ShouldThrowNotFoundException_WhenWorkerDoesNotExist()
    {
        var skillId = 0;
        var model = new SkillUpdateModel(1, 2);

        _workerRepoMock.Setup(x => x.Exists(
            model.WorkerId!.Value,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.UpdateSkill(skillId, model, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _skillRepoMock.Verify(x => x.Update(
            It.IsAny<int>(),
            It.IsAny<SkillUpdateModel>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task UpdateSkill_ShouldThrowNotFoundException_WhenSpecializationDoesNotExist()
    {
        var skillId = 0;
        var model = new SkillUpdateModel(1, 2);

        _workerRepoMock.Setup(x => x.Exists(
            model.WorkerId!.Value,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        _specRepoMock.Setup(x => x.Exists(
            model.SpecializationId!.Value,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.UpdateSkill(skillId, model, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _skillRepoMock.Verify(x => x.Update(
            It.IsAny<int>(),
            It.IsAny<SkillUpdateModel>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task GetPagedSkills_WhenUserIsNotManager_ShouldForceFilterByOwnProfileId()
    {
        var inputFilter = new SkillFilter(
            [1, 2, 3],
            [],
            null,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(3);


        _skillRepoMock.Setup(x => x.Get(
            It.IsAny<SkillFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SkillItem>());

        await _service.GetSkills(inputFilter, CancellationToken.None);

        _skillRepoMock.Verify(x => x.Get(
                It.Is<SkillFilter>(f => f.WorkerIds!.Count() == 1 && f.WorkerIds!.First() == 10),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }

    [Fact]
    public async Task GetPagedSkills_WhenUserIsManager_ShouldNotForceFilterByOwnProfileId()
    {
        var inputFilter = new SkillFilter(
            [1, 2, 3],
            [],
            null,
            true);

        _userContextMock.Setup(x => x.ProfileId).Returns(10);
        _userContextMock.Setup(x => x.RoleId).Returns(1);


        _skillRepoMock.Setup(x => x.Get(
            It.IsAny<SkillFilter>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<SkillItem>());

        await _service.GetSkills(inputFilter, CancellationToken.None);

        _skillRepoMock.Verify(x => x.Get(
                It.Is<SkillFilter>(f => f.WorkerIds!.Count() != 1 && f.WorkerIds!.First() == 1),
                It.IsAny<CancellationToken>()),
                Times.Once);
    }
}
