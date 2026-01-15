using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class ClientServiceTests
{
    private readonly Mock<IClientRepository> _clientRepoMock;
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<ILogger<ClientService>> _loggerMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly ClientService _service;

    public ClientServiceTests()
    {
        _clientRepoMock = new Mock<IClientRepository>();
        _userRepoMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<ClientService>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new ClientService(
            _clientRepoMock.Object,
            _userRepoMock.Object,
            _loggerMock.Object,
            _unitOfWorkMock.Object);
    }

    [Fact]

    public async Task CreateClientWithUser_ShouldRollback_WhenUserCreationFails()
    {
        var userId = 123;
        var clientId = 123;

        var (user, errorsUser) = User.Create(
            userId,
            (int)RoleEnum.Client,
            "ClientUser",
            "TestTestTest");

        user.Should().NotBeNull();
        errorsUser.Should().BeEmpty();

        var (client, errorsClient) = Client.Create(
            clientId,
            user.Id,
            "TestTest",
            "TestTestTest",
            "80444444444",
            "TEstTestTest");

        client.Should().NotBeNull();
        errorsClient.Should().BeEmpty();

        _userRepoMock.Setup(x => x.Create(
                          user,
                          It.IsAny<CancellationToken>()))
                        .ReturnsAsync(userId);

        _clientRepoMock.Setup(x => x.Create(
                          client!,
                          It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new Exception("Some think going wrong"));

        await Assert.ThrowsAsync<Exception>(() =>
        _service.CreateClientWithUser(client!, user, It.IsAny<CancellationToken>()));

        _userRepoMock.Verify(x => x.Create(
                       user,
                       It.IsAny<CancellationToken>()),
                       Times.Once);

        _clientRepoMock.Verify(x => x.Create(
                         client!,
                         It.IsAny<CancellationToken>()),
                         Times.Once);

        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(
                 It.IsAny<CancellationToken>()),
                Times.Once);

        _unitOfWorkMock.Verify(x => x.CommitTransactionAsync(
                         It.IsAny<CancellationToken>()),
                        Times.Never);

        _unitOfWorkMock.Verify(x => x.RollbackAsync(
                         It.IsAny<CancellationToken>()),
                        Times.Once);
    }
}
