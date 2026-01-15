using CRMSystem.Business.Services;
using CRMSystem.Core.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.Core.Models;
using FluentAssertions;
using Moq;

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
        var (order, errors) = Order.Create(
                        123,
                        OrderStatusEnum.Accepted,
                        123,
                        new DateOnly(2025, 1 ,1), 
                        OrderPriorityEnum.Medium);

        errors.Should().BeEmpty();
        order.Should().NotBeNull();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.CreateOrder(order, It.IsAny<CancellationToken>()));

        _orderRepoMock.Verify(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CreateOrder_ShouldThrowNotFoundException_WhenPriorityDoesNotExist()
    {
        var (order, errors) = Order.Create(
                        123,
                        OrderStatusEnum.Accepted,
                        123,
                        new DateOnly(2025, 1, 1),
                        OrderPriorityEnum.Medium);

        errors.Should().BeEmpty();
        order.Should().NotBeNull();

        _carRepoMock.Setup(x => x.Exists(
                        order.CarId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true);

        _orderPriorityRepoMock.Setup(x => x.Exists(
                        (int)order.PriorityId,
                        It.IsAny<CancellationToken>()))
                    .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.CreateOrder(order, It.IsAny<CancellationToken>()));

        _orderRepoMock.Verify(x => x.Create(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CreateOrder_ShouldThrowNotFoundException_WhenStatusDoesNotExist()
    {
        var (order, errors) = Order.Create(
                         123,
                         OrderStatusEnum.Accepted,
                         123,
                         new DateOnly(2025, 1, 1),
                         OrderPriorityEnum.Medium);

        errors.Should().BeEmpty();
        order.Should().NotBeNull();

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

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.CreateOrder(order, It.IsAny<CancellationToken>()));

        _orderRepoMock.Verify(x => x.Create(
                        It.IsAny<Order>(),
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CreateOrder_WhenCarExistsAndOrderPriorityExistsAndStatusExists_ShouldReturnId()
    {
        var orderId = 123;
        var (order, errors) = Order.Create(
                         orderId,
                         OrderStatusEnum.Accepted,
                         123,
                         new DateOnly(2025, 1, 1),
                         OrderPriorityEnum.Medium);

        errors.Should().BeEmpty();
        order.Should().NotBeNull();

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

        var result = await _service.CreateOrder(
                            order, 
                            CancellationToken.None);

        Assert.Equal(result, orderId);

        _orderRepoMock.Verify(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()),
                       Times.Once);
    }

    [Fact]
    public async Task CreateOrderWithBill_ShouldRollback_WhenBillCreationFails()
    {
        var orderId = 123L;
        var billId = 123;
        var (order, errorsOrder) = Order.Create(
                         orderId,
                         OrderStatusEnum.Accepted,
                         123,
                         new DateOnly(2025, 1, 1),
                         OrderPriorityEnum.Medium);

        errorsOrder.Should().BeEmpty();
        order.Should().NotBeNull();

        var (bill, errorsBill) = Bill.Create(
                        billId, 
                        orderId, 
                        BillStatusEnum.PartiallyPaid, 
                        new DateTime(2025, 1, 1, 12, 12, 12, 12, 12, DateTimeKind.Utc), 
                        123, 
                        null);

        errorsBill.Should().BeEmpty();
        bill.Should().NotBeNull();

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

        await Assert.ThrowsAsync<Exception>(() => 
            _service.CreateOrderWithBill(order, bill, CancellationToken.None));

        _orderRepoMock.Verify(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()),
                       Times.Once);

        _billRepoMock.Verify(x => x.Create(
                        bill,
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
        var orderId = 123L;
        var billId = 123;
        var (order, errorsOrder) = Order.Create(
                         orderId,
                         OrderStatusEnum.Accepted,
                         123,
                         new DateOnly(2025, 1, 1),
                         OrderPriorityEnum.Medium);

        errorsOrder.Should().BeEmpty();
        order.Should().NotBeNull();

        var (bill, errorsBill) = Bill.Create(
                        billId,
                        orderId,
                        BillStatusEnum.PartiallyPaid,
                        new DateTime(2025, 1, 1, 12, 12, 12, 12, 12, DateTimeKind.Utc),
                        123,
                        null);

        errorsBill.Should().BeEmpty();
        bill.Should().NotBeNull();

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

        Assert.Equal(orderId, result);

        _orderRepoMock.Verify(x => x.Create(
                        order,
                        It.IsAny<CancellationToken>()),
                       Times.Once);

        _billRepoMock.Verify(x => x.Create(
                        bill,
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

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.UpdateOrder(orderId, priorityId, CancellationToken.None));

        _orderRepoMock.Verify(x => x.Update(
                        orderId,
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

        Assert.Equal(result, orderId);

        _orderRepoMock.Verify(x => x.Update(
                        orderId,
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

        Assert.Equal(result, orderId);

        _orderRepoMock.Verify(x => x.Delete(
                        orderId,
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

        await Assert.ThrowsAsync<ConflictException>(() =>
            _service.CloseOrder(orderId, CancellationToken.None));

        _orderRepoMock.Verify(x => x.Close(
                        orderId,
                        It.IsAny<CancellationToken>()),
                        Times.Never);
    }

    [Fact]
    public async Task CloseOrder_WhenAnyWorkInProgress_ShouldReturnId()
    {
        var orderId = 123L;

        _orderRepoMock.Setup(x => x.PossibleToClose(
                        orderId,
                        It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        _orderRepoMock.Setup(x => x.Close(
                        orderId,
                        It.IsAny<CancellationToken>()))
                        .ReturnsAsync(orderId);

        var result = await _service.CloseOrder(orderId, CancellationToken.None);

        Assert.Equal(result, orderId);

        _orderRepoMock.Verify(x => x.Close(
                        orderId,
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

        await Assert.ThrowsAsync<ConflictException>(() =>
            _service.CompleteOrder(orderId, CancellationToken.None));

        _orderRepoMock.Verify(x => x.Complete(
                        orderId,
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

        Assert.Equal(result, orderId);

        _orderRepoMock.Verify(x => x.Complete(
                        orderId,
                        It.IsAny<CancellationToken>()),
                        Times.Once);
    }
}
