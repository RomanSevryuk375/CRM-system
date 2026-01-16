using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.Core.Exceptions;
using CRMSystem.DataAccess.Entites;
using FluentAssertions;

namespace CRMSystem.Business.Tests.IntegrationTests;

public class OrderIntegrationTests : BaseIntegrationTest, IClassFixture<IntegrationTestFactory>
{
    private readonly IOrderService _orderService;

    public OrderIntegrationTests(IntegrationTestFactory factory) : base(factory)
    {
        _orderService = serviceProvider.GetRequiredService<IOrderService>();
    }

    [Fact]
    public async Task CloseOrder_ShouldThrowConflictException_WhenBillUnPaid()
    {
        var userWorker = new UserEntity
        {
            RoleId = (int)RoleEnum.Worker,
            Login = "Workerlollollol",
            PasswordHash = "lollollol227"
        };
        var userClient = new UserEntity
        {
            RoleId = (int)RoleEnum.Worker,
            Login = "Clientlollollol",
            PasswordHash = "lollollol228"
        };

        dbContext.Users.Add(userClient);
        dbContext.Users.Add(userWorker);

        await dbContext.SaveChangesAsync();

        var worker = new WorkerEntity
        {
            UserId = userWorker.Id,
            Name = "name",
            Surname = "surname",
            HourlyRate = 50,
            PhoneNumber = "8044591940",
            Email = "email"
        };
        var client = new ClientEntity
        {
            UserId = userClient.Id,
            Name = "name228",
            Surname = "surname288",
            PhoneNumber = "8044591941",
            Email = "email228"
        };

        dbContext.Workers.Add(worker);
        dbContext.Clients.Add(client);

        await dbContext.SaveChangesAsync();

        var car = new CarEntity
        {
            OwnerId = client.Id,
            StatusId = (int)CarStatusEnum.AtWork,
            Brand = "brand",
            Model = "model",
            YearOfManufacture = 2008,
            VinNumber = "VF3MJAHXVHS101043",
            StateNumber = "1111AA-1",
            Mileage = 15000,
        };

        dbContext.Cars.Add(car);

        await dbContext.SaveChangesAsync();

        var work = new WorkEntity
        {
            Title = "title",
            Category = "category",
            Description = "description",
            StandardTime = 1,
        };
        var order = new OrderEntity
        {
            StatusId = (int)OrderStatusEnum.InProgress,
            CarId = car.Id,
            Date = new DateOnly(2025, 1, 1),
            PriorityId = (int)OrderPriorityEnum.Medium
        };

        dbContext.Works.Add(work);
        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync();

        var bill = new BillEntity
        {
            OrderId = order.Id,
            StatusId = (int)BillStatusEnum.Paid,
            CreatedAt = new DateTime(2025, 1, 1),
            Amount = 100,
            ActualBillDate = null
        };

        dbContext.Bills.Add(bill);

        dbContext.WorksInOrder.Add(new WorkInOrderEntity
        {

            OrderId = order.Id,
            JobId = work.Id,
            WorkerId = worker.Id,
            StatusId = (int)WorkStatusEnum.Completed,
            TimeSpent = 2,
        });

        await dbContext.SaveChangesAsync();

        dbContext.ChangeTracker.Clear();

        var act = () => _orderService.CloseOrder(order.Id, default);

        await act.Should().ThrowAsync<ConflictException>();
    }

    [Fact]
    public async Task CompleteOrder_ShouldThrowConflictException_WhenWorksNotComplete()
    {
        var userWorker = new UserEntity
        {
            RoleId = (int)RoleEnum.Worker,
            Login = "Workerlollollol",
            PasswordHash = "lollollol227"
        };
        var userClient = new UserEntity
        {
            RoleId = (int)RoleEnum.Worker,
            Login = "Clientlollollol",
            PasswordHash = "lollollol228"
        };

        dbContext.Users.Add(userClient);
        dbContext.Users.Add(userWorker);

        await dbContext.SaveChangesAsync();

        var worker = new WorkerEntity
        {
            UserId = userWorker.Id,
            Name = "name",
            Surname = "surname",
            HourlyRate = 50,
            PhoneNumber = "8044591940",
            Email = "email"
        };
        var client = new ClientEntity
        {
            UserId = userClient.Id,
            Name = "name228",
            Surname = "surname288",
            PhoneNumber = "8044591941",
            Email = "email228"
        };

        dbContext.Workers.Add(worker);
        dbContext.Clients.Add(client);

        await dbContext.SaveChangesAsync();

        var car = new CarEntity
        {
            OwnerId = client.Id,
            StatusId = (int)CarStatusEnum.AtWork,
            Brand = "brand",
            Model = "model",
            YearOfManufacture = 2008,
            VinNumber = "VF3MJAHXVHS101043",
            StateNumber = "1111AA-1",
            Mileage = 15000,
        };

        dbContext.Cars.Add(car);

        await dbContext.SaveChangesAsync();

        var work = new WorkEntity
        {
            Title = "title",
            Category = "category",
            Description = "description",
            StandardTime = 1,
        };
        var order = new OrderEntity
        {
            StatusId = (int)OrderStatusEnum.InProgress,
            CarId = car.Id,
            Date = new DateOnly(2025, 1, 1),
            PriorityId = (int)OrderPriorityEnum.Medium
        };

        dbContext.Works.Add(work);
        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync();

        var bill = new BillEntity
        {
            OrderId = order.Id,
            StatusId = (int)BillStatusEnum.Paid,
            CreatedAt = new DateTime(2025, 1, 1),
            Amount = 100,
            ActualBillDate = null
        };

        dbContext.Bills.Add(bill);

        dbContext.WorksInOrder.Add(new WorkInOrderEntity
        {

            OrderId = order.Id,
            JobId = work.Id,
            WorkerId = worker.Id,
            StatusId = (int)WorkStatusEnum.InProgress,
            TimeSpent = 2,
        });

        await dbContext.SaveChangesAsync();

        dbContext.ChangeTracker.Clear();

        var act = () => _orderService.CompleteOrder(order.Id, default);

        await act.Should().ThrowAsync<ConflictException>();
    }
}
