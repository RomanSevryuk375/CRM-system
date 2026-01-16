using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class GuaranteeServiceTests
{
    private readonly Mock<IGuaranteeRepository> _guaranteeRepoMock;
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<ILogger<GuaranteeService>> _loggerMock;
    private readonly GuaranteeService _service;

    public GuaranteeServiceTests()
    {
        _guaranteeRepoMock = new Mock<IGuaranteeRepository>();
        _orderRepoMock = new Mock<IOrderRepository>();
        _loggerMock = new Mock<ILogger<GuaranteeService>>();

        _service = new GuaranteeService(
            _guaranteeRepoMock.Object,
            _orderRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateGuarantee_ShouldThrowNotFoundException_WhenOrderDoesNotExist()
    {
        var guarantee = ValidObjects.CreateValidGuarantee();

        _orderRepoMock.Setup(x => x.Exists(
                        guarantee.OrderId,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);

        var act = () => _service.CreateGuarantee(guarantee, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _guaranteeRepoMock.Verify(x => x.Create(
                            It.IsAny<Guarantee>(),
                            It.IsAny<CancellationToken>()),
                            Times.Never);
    }

    [Fact]
    public async Task CreateGuarantee_WhenOrderExist_ShouldReturnID()
    {
        var guaranteeId = 0;
        var guarantee = ValidObjects.CreateValidGuarantee();

        _orderRepoMock.Setup(x => x.Exists(
                        guarantee.OrderId,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _guaranteeRepoMock.Setup(x => x.Create(
                            guarantee,
                            It.IsAny<CancellationToken>()))
                            .ReturnsAsync(guaranteeId);

        var act = await _service.CreateGuarantee(guarantee, CancellationToken.None);

        act.Should().Be(guaranteeId);

        _guaranteeRepoMock.Verify(x => x.Create(
                            It.IsAny<Guarantee>(),
                            It.IsAny<CancellationToken>()),
                            Times.Once);
    }
}
