using CRMSystem.Business.Abstractions;
using CRMSystem.Core.Enums;
using CRMSystem.DataAccess.Entites;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CRMSystem.Business.Tests.IntegrationTests;

public class WorkProposalServiceIntegrationTests : BaseIntegrationTest, IClassFixture<IntegrationTestFactory>
{
    private readonly IWorkProposalService _workProposalService;

    public WorkProposalServiceIntegrationTests(IntegrationTestFactory factory) : base(factory)
    {
        _workProposalService = serviceProvider.GetRequiredService<IWorkProposalService>();
    }

    [Fact]
    public async Task AcceptProposal_ShouldRecalculateBillAndMoveWorksAndMovePartsAndDeleteProposal()
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

        var partCategory = new PartCategoryEntity
        {
            Name = "name",
            Description = "description"
        };

        dbContext.PartCategories.Add(partCategory);
        await dbContext.SaveChangesAsync();

        var part = new PartEntity
        {
            CategoryId = partCategory.Id,
            OEMArticle = "oemArticle",
            ManufacturerArticle = "manufacturerArticle",
            Manufacturer = "manufacturer",
            InternalArticle = "internalArticle",
            Description = "description",
            Name = "name",
            Applicability = "applicability"
        };

        dbContext.Parts.Add(part);
        await dbContext.SaveChangesAsync();

        var cell = new StorageCellEntity
        {
            Rack = "rack",
            Shelf = "shelf"
        };

        dbContext.StorageCells.Add(cell);
        await dbContext.SaveChangesAsync();

        var position = new PositionEntity
        {
            PartId = part.Id,
            CellId = cell.Id,
            PurchasePrice = 50,
            SellingPrice = 100,
            Quantity = 1,
        };

        dbContext.Positions.Add(position);
        await dbContext.SaveChangesAsync();

        var proposal = new WorkProposalEntity
        {
            OrderId = order.Id,
            JobId = work.Id,
            WorkerId = worker.Id,
            StatusId = (int)ProposalStatusEnum.Pending,
            Date = new DateTime(2025, 1, 1)
        };

        dbContext.WorkProposals.Add(proposal);
        await dbContext.SaveChangesAsync();

        var partset = new PartSetEntity
        (
            null,
            position.Id,
            proposal.Id,
            1,
            120
        );

        dbContext.PartSets.Add(partset);
        await dbContext.SaveChangesAsync();

        dbContext.ChangeTracker.Clear();

        await _workProposalService.AcceptProposal(proposal.Id, default);

        var updatedBill = await dbContext.Bills.FindAsync(bill.Id);
        updatedBill.Should().NotBeNull();
        updatedBill.Amount.Should().Be(120);

        var wioCount = dbContext.WorksInOrder.Count();
        wioCount.Should().Be(1);

        var movedPartSet = await dbContext.PartSets.FirstOrDefaultAsync(ps => ps.PositionId == position.Id);
        movedPartSet!.OrderId.Should().Be(order.Id);     
        movedPartSet!.ProposalId.Should().BeNull();   

        var deletedProposal = dbContext.WorkProposals.Count();
        deletedProposal.Should().Be(0);
    }

    [Fact]
    public async Task RejectProposal_ShouldDeleteProposedPartSetAndDeleteProposal()
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

        var partCategory = new PartCategoryEntity
        {
            Name = "name",
            Description = "description"
        };

        dbContext.PartCategories.Add(partCategory);
        await dbContext.SaveChangesAsync();

        var part = new PartEntity
        {
            CategoryId = partCategory.Id,
            OEMArticle = "oemArticle",
            ManufacturerArticle = "manufacturerArticle",
            Manufacturer = "manufacturer",
            InternalArticle = "internalArticle",
            Description = "description",
            Name = "name",
            Applicability = "applicability"
        };

        dbContext.Parts.Add(part);
        await dbContext.SaveChangesAsync();

        var cell = new StorageCellEntity
        {
            Rack = "rack",
            Shelf = "shelf"
        };

        dbContext.StorageCells.Add(cell);
        await dbContext.SaveChangesAsync();

        var position = new PositionEntity
        {
            PartId = part.Id,
            CellId = cell.Id,
            PurchasePrice = 50,
            SellingPrice = 100,
            Quantity = 1,
        };

        dbContext.Positions.Add(position);
        await dbContext.SaveChangesAsync();

        var proposal = new WorkProposalEntity
        {
            OrderId = order.Id,
            JobId = work.Id,
            WorkerId = worker.Id,
            StatusId = (int)ProposalStatusEnum.Pending,
            Date = new DateTime(2025, 1, 1)
        };

        dbContext.WorkProposals.Add(proposal);
        await dbContext.SaveChangesAsync();

        var partset = new PartSetEntity
        (
            null,
            position.Id,
            proposal.Id,
            1,
            120
        );

        dbContext.PartSets.Add(partset);
        await dbContext.SaveChangesAsync();

        dbContext.ChangeTracker.Clear();

        await _workProposalService.RejectProposal(proposal.Id, default);

        var movedPartSet = dbContext.PartSets.Count();
        movedPartSet.Should().Be(0);

        var deletedProposal = dbContext.WorkProposals.Count();
        deletedProposal.Should().Be(0);
    }
}
