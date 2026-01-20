using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using FluentAssertions;
using Shared.Enums;

namespace CRMSystem.Business.Tests.IntegrationTests;

public class WorkInOrderServiceIntegrationTests : BaseIntegrationTest, IClassFixture<IntegrationTestFactory>
{
    private readonly IWorkInOrderService _wioService;

    public WorkInOrderServiceIntegrationTests(IntegrationTestFactory factory) : base(factory)
    {
        _wioService = serviceProvider.GetRequiredService<IWorkInOrderService>();
    }

    [Fact]
    public async Task CreateWiO_ShouldRecalculateBillAmount_WhenExistsWorks()
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
            StatusId = (int)BillStatusEnum.Unpaid,
            CreatedAt = new DateTime(2025, 1, 1),
            Amount = 0,
            ActualBillDate = null
        };

        dbContext.Bills.Add(bill);
        await dbContext.SaveChangesAsync();

        dbContext.ChangeTracker.Clear();

        await _wioService.CreateWiO(WorkInOrder.Create(
            0,
            order.Id,
            work.Id,
            worker.Id,
            WorkStatusEnum.InProgress,
            2).workInOrder!, default);

        var updatedBill = await dbContext.Bills.FindAsync(bill.Id);

        updatedBill.Should().NotBeNull();
        updatedBill.Amount.Should().Be(100);
    }
}
