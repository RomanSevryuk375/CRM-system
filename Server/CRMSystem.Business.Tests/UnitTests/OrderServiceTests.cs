using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;
using Order = CRMSystem.Core.Models.Order;

namespace CRMSystem.Business.Tests.UnitTests;

public class OrderServiceTests
{
    private readonly Mock<IOrderRepository> _orderRepoMock;
    private readonly Mock<IOrderStatusRepository> _orderStatusRepoMock;
    private readonly Mock<ICarRepository> _carRepoMock;
    private readonly Mock<IOrderPriorityRepository> _orderPriorityRepoMock;
    private readonly Mock<IBillRepository> _billRepoMock;
    private readonly Mock<ILogger<OrderService>> _loggerRepoMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly OrderService _service;

    public OrderServiceTests()
    {
        _orderRepoMock = new Mock<IOrderRepository>();
        _orderStatusRepoMock = new Mock<IOrderStatusRepository>();
        _carRepoMock = new Mock<ICarRepository>();
        _orderPriorityRepoMock = new Mock<IOrderPriorityRepository>();
        _billRepoMock = new Mock<IBillRepository>();
        _loggerRepoMock = new Mock<ILogger<OrderService>>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _service = new OrderService(
            _orderRepoMock.Object,
            _orderStatusRepoMock.Object,
            _carRepoMock.Object,
            _orderPriorityRepoMock.Object,
            _billRepoMock.Object,
            _loggerRepoMock.Object,
            _unitOfWorkMock.Object);

    }

    [Fact]
    public async Task CreateOrder_ShouldThrowNotFoundException_WhenCarDoesNotExist()
    {
        var order = ValidObjects.CreateValidOrder();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        var act = () => _service.CreateOrder(order, It.IsAny<CancellationToken>());

        await act.Should().ThrowAsync<NotFoundException>();

        _orderRepoMock.Verify(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CreateOrder_ShouldThrowNotFoundException_WhenPriorityDoesNotExist()
    {
        var order = ValidObjects.CreateValidOrder();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)order.PriorityId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        var act = () => _service.CreateOrder(order, It.IsAny<CancellationToken>());

        await act.Should().ThrowAsync<NotFoundException>();

        _orderRepoMock.Verify(x => x.Create(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CreateOrder_ShouldThrowNotFoundException_WhenStatusDoesNotExist()
    {
        var order = ValidObjects.CreateValidOrder();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)order.PriorityId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderStatusRepoMock.Setup(x => x.Exists(
                        (int)order.StatusId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        var act = () => _service.CreateOrder(order, It.IsAny<CancellationToken>());

        await act.Should().ThrowAsync<NotFoundException>();

        _orderRepoMock.Verify(x => x.Create(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CreateOrder_WhenCarExistsAndOrderPriorityExistsAndStatusExists_ShouldReturnId()
    {
        var orderId = 0;
        var order = ValidObjects.CreateValidOrder();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)order.PriorityId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderStatusRepoMock.Setup(x => x.Exists(
                        (int)order.StatusId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(orderId);

        var result = await _service.CreateOrder(order, CancellationToken.None);

        result.Should().Be(orderId);

        _orderRepoMock.Verify(x => x.Create(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()),
                       Times.Once);
    }

    [Fact]
    public async Task CreateOrderWithBill_ShouldRollback_WhenBillCreationFails()
    {
        var orderId = 1;
        var order = ValidObjects.CreateValidOrder();

        var bill = ValidObjects.CreateValidBill();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)order.PriorityId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderStatusRepoMock.Setup(x => x.Exists(
                        (int)order.StatusId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(orderId);

        _billRepoMock.Setup(x => x.Create(
                        It.IsAny<Bill>(),
                        It.IsAny<CancellationToken>()))
                       .ThrowsAsync(new Exception("Some think going wrong"));  

        var act = () => _service.CreateOrderWithBill(order, bill, CancellationToken.None);

        await act.Should().ThrowAsync<Exception>();

        _orderRepoMock.Verify(x => x.Create(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()),
                       Times.Once);

        _billRepoMock.Verify(x => x.Create(
                        It.IsAny<Bill>(),
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

    [Fact]
    public async Task CreateOrderWithBill_WhenCarExistsAndOrderPriorityExistsAndStatusExists_ShouldReturnId()
    {
        var orderId = 1;
        var billId = 0;
        var order = ValidObjects.CreateValidOrder();

        var bill = ValidObjects.CreateValidBill();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)order.PriorityId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderStatusRepoMock.Setup(x => x.Exists(
                        (int)order.StatusId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(orderId);

        _billRepoMock.Setup(x => x.Create(
                        It.IsAny<Bill>(),
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(billId);

        var result = await _service.CreateOrderWithBill(order, bill, CancellationToken.None);

        result.Should().Be(orderId);

        _orderRepoMock.Verify(x => x.Create(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()),
                       Times.Once);

        _billRepoMock.Verify(x => x.Create(
                        It.IsAny<Bill>(),
                        It.IsAny<CancellationToken>()),
                       Times.Once);

        _unitOfWorkMock.Verify(x => x.BeginTransactionAsync(
                         It.IsAny<CancellationToken>()),
                        Times.Once);

        _unitOfWorkMock.Verify(x => x.CommitTransactionAsync(
                         It.IsAny<CancellationToken>()),
                        Times.Once);

        _unitOfWorkMock.Verify(x => x.RollbackAsync(
                         It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task UpdateOrder_ShouldThrowNotFoundException_WhenPriorityDoesMotExist()
    {
        var orderId = 123L;
        var priorityId = OrderPriorityEnum.Medium;

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)priorityId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        var act = () => _service.UpdateOrder(orderId, priorityId, CancellationToken.None);

        await act.Should().ThrowAsync<NotFoundException>();

        _orderRepoMock.Verify(x => x.Update(
                        It.IsAny<long>(),
                        priorityId,
                        It.IsAny<CancellationToken>()),
                        Times.Never);                       
    }

    [Fact]
    public async Task UpdateOrder_WhenOrderPriorityExists_ShouldReturnId()
    {
        var orderId = 123L;
        var priorityId = OrderPriorityEnum.Medium;

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)priorityId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.Update(
                orderId,
                priorityId,
                It.IsAny<CancellationToken>()))
               .ReturnsAsync(orderId);

        var result = await _service.UpdateOrder(orderId, priorityId, CancellationToken.None);

        result.Should().Be(orderId);

        _orderRepoMock.Verify(x => x.Update(
                        It.IsAny<long>(),
                        priorityId,
                        It.IsAny<CancellationToken>()),
                        Times.Once);
    }

    [Fact]
    public async Task DeleteOrder_ShouldReturnId()
    {
        var orderId = 123L;

        _orderRepoMock.Setup(x => x.Delete(
                orderId,
                It.IsAny<CancellationToken>()))
               .ReturnsAsync(orderId);

        var result = await _service.DeleteOrder(orderId, CancellationToken.None);

        result.Should().Be(orderId);

        _orderRepoMock.Verify(x => x.Delete(
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()),
                        Times.Once);
    }

    [Fact]
    public async Task CloseOrder_ShouldThrowConflictException_WhenHaveGotBillWithUnPaidStatus()
    {
        var orderId = 123L;

        _orderRepoMock.Setup(x => x.PossibleToClose(
                        orderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        var act = () => _service.CloseOrder(orderId, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _orderRepoMock.Verify(x => x.Close(
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CloseOrder_WhenAnyWorkInProgress_ShouldReturnId()
    {
        var orderId = 123L;

        _orderRepoMock.Setup(x => x.PossibleToComplete(
                        orderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.PossibleToClose(
                        orderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.Close(
                        orderId,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(orderId);

        var result = await _service.CloseOrder(orderId, CancellationToken.None);

        result.Should().Be(orderId);

        _orderRepoMock.Verify(x => x.Close(
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()),
                        Times.Once);
    }

    [Fact]
    public async Task CompleteOrder_ShouldThrowConflictException_WhenHaveGotWorkInProgress()
    {
        var orderId = 123L;

        _orderRepoMock.Setup(x => x.PossibleToComplete(
                        orderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(false);

        var act = () => _service.CompleteOrder(orderId, CancellationToken.None);

        await act.Should().ThrowAsync<ConflictException>();

        _orderRepoMock.Verify(x => x.Complete(
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CompleteOrder_WhenBillDebtIsZero_ShouldReturnId()
    {
        var orderId = 123L;

        _orderRepoMock.Setup(x => x.PossibleToComplete(
                        orderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.Complete(
                        orderId,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(orderId);

        var result = await _service.CompleteOrder(orderId, CancellationToken.None);

        result.Should().Be(orderId);

        _orderRepoMock.Verify(x => x.Complete(
                        It.IsAny<long>(),
                        It.IsAny<CancellationToken>()),
                        Times.Once);
    }
}
