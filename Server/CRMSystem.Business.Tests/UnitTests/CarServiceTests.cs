using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

namespace CRMSystem.Business.Tests.UnitTests;

public class CarServiceTests
{
    private readonly Mock<ICarRepository> _carRepoMock;
    private readonly Mock<IClientRepository> _clientRepoMock;
    private readonly Mock<ICarStatusRepository> _carStatusRepoMock;
    private readonly Mock<ILogger<CarService>> _loggerMock;
    private readonly CarService _service;

    public CarServiceTests()
    {
        _carRepoMock = new Mock<ICarRepository>();
        _clientRepoMock = new Mock<IClientRepository>();
        _carStatusRepoMock = new Mock<ICarStatusRepository>();
        _loggerMock = new Mock<ILogger<CarService>>();

        _service = new CarService(
            _carRepoMock.Object,
            _clientRepoMock.Object,
            _carStatusRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateCar_ShouldThrowNotFoundException_WhenClientDoesNotExists()
    {
        var car = ValidObjects.CreateValidCar(CarStatusEnum.NotAtWork);

        _clientRepoMock.Setup(x => x.Exists(
                            car.OwnerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);

        var act = () => _service.CreateCar(car, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _carRepoMock.Verify(x => x.Create(
                       It.IsAny<Car>(),
                       It.IsAny<CancellationToken>()),
                       Times.Never);
    }

    [Fact]
    public async Task CreateCar_ShouldThrowNotFoundException_WhenCarStatusDoesNotExists()
    {
        var car = ValidObjects.CreateValidCar(CarStatusEnum.NotAtWork);

        _clientRepoMock.Setup(x => x.Exists(
                            car.OwnerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _carStatusRepoMock.Setup(x => x.Exists(
                            (int)car.StatusId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);

        var act = () => _service.CreateCar(car, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _carRepoMock.Verify(x => x.Create(
                       It.IsAny<Car>(),
                       It.IsAny<CancellationToken>()),
                       Times.Never);
    }

    [Fact]
    public async Task CreateCar_ShouldThrowNotFoundException_WhenCarStatusIsAtWork()
    {
        var car = ValidObjects.CreateValidCar(CarStatusEnum.AtWork);

        _clientRepoMock.Setup(x => x.Exists(
                            car.OwnerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _carStatusRepoMock.Setup(x => x.Exists(
                            (int)car.StatusId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        var act = () => _service.CreateCar(car, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _carRepoMock.Verify(x => x.Create(
                       It.IsAny<Car>(),
                       It.IsAny<CancellationToken>()),
                       Times.Never);
    }

    [Fact]
    public async Task CreateCar_WhenCarStatusIsNotAtWorkAndClientExistsAndStatusExists_ShouldReturnId()
    {
        var carId = 0;
        var car = ValidObjects.CreateValidCar(CarStatusEnum.NotAtWork); 

        _clientRepoMock.Setup(x => x.Exists(
                            car.OwnerId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _carStatusRepoMock.Setup(x => x.Exists(
                            (int)car.StatusId,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        _carRepoMock.Setup(x => x.Create(
                            car,
                            It.IsAny<CancellationToken>()))
                        .ReturnsAsync(carId);

        var result = await _service.CreateCar(car, CancellationToken.None);

        result.Should().Be(carId);

        _carRepoMock.Verify(x => x.Create(
                       It.IsAny<Car>(),
                       It.IsAny<CancellationToken>()),
                       Times.Once);
    }
}
