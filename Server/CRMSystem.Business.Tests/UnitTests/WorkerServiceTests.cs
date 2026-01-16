using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class WorkerServiceTests
{
    private readonly Mock<IWorkerRepository> _workerRepoMock;
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<ILogger<WorkerService>> _loggerMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly WorkerService _service;

    public WorkerServiceTests()
    {
        _workerRepoMock = new Mock<IWorkerRepository>();
        _userRepoMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<WorkerService>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new WorkerService(
            _workerRepoMock.Object,
            _userRepoMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]
    public async Task CreateWorker_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        var worker = ValidObjects.CreateValidWorker();

        _userRepoMock.Setup(x => x.Exists(
            worker.UserId,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = () => _service.CreateWorker(worker, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _workerRepoMock.Verify(x => x.Create(
            It.IsAny<Worker>(),
            It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task CreateWorkerWithUser_ShouldRollback_WhenWorkerRepoFails()
    {
        var userId = 777;

        var user = ValidObjects.CreateValidWorkerUser();
        var worker = ValidObjects.CreateValidWorker();
        
        _userRepoMock.Setup(x => x.Create(
            user,
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(userId);

        _workerRepoMock.Setup(x => x.Create(
            worker, 
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Database crash"));

        var act = () => _service.CreateWorkerWithUser(worker, user, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();

        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(
            It.IsAny<CancellationToken>()), 
            Times.Once);

        _unitOfWorkMock.Verify(x => x.RollbackAsync(
            It.IsAny<CancellationToken>()), 
            Times.Once);

        _unitOfWorkMock.Verify(x => x.CommitTransactionAsync(
            It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
