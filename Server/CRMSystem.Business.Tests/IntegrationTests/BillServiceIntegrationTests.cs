using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;

namespace CRMSystem.Business.Tests.IntegrationTests;

public class BillServiceIntegrationTests : BaseIntegrationTest, IClassFixture<IntegrationTestFactory>
{
    private readonly IBillService _billService;

    public BillServiceIntegrationTests(IntegrationTestFactory factory) : base(factory)
    {
        _billService = serviceProvider.GetRequiredService<IBillService>();
    }

    [Fact]
    public async Task RecalculateAmount_ShouldUpdateBill_WhenPartsAndWorksExist()
    {
        var userWorker = new UserEntity {
            RoleId = (int)RoleEnum.Worker,
            Login = "Workerlollollol",
            PasswordHash = "lollollol227"
        };
        var userClient = new UserEntity {
            RoleId = (int)RoleEnum.Worker,
            Login = "Clientlollollol",
            PasswordHash = "lollollol228"
        };

        dbContext.Users.Add(userClient);
        dbContext.Users.Add(userWorker);

        await dbContext.SaveChangesAsync();

        var worker = new WorkerEntity {
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

        var work = new WorkEntity {
            Title = "title",
            Category = "category",
            Description = "description",
            StandardTime = 2,
        };
        var order = new OrderEntity {
            StatusId = (int)OrderStatusEnum.InProgress,
            CarId = car.Id,
            Date = new DateOnly(2025, 1, 1),
            PriorityId = (int)OrderPriorityEnum.Medium
        };

        dbContext.Works.Add(work);
        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync();

        var bill = new BillEntity {
            OrderId = order.Id,
            StatusId = (int)BillStatusEnum.Unpaid,
            CreatedAt = new DateTime(2025, 1, 1),
            Amount = 0,
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

        await _billService.RecalculateBillAmount(bill.Id, CancellationToken.None);

        dbContext.ChangeTracker.Clear();

        var updatedBill = await dbContext.Bills.FindAsync(bill.Id);

        Assert.Equal(100, updatedBill.Amount);
    }
}