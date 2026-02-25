using CRMSystem.Core.Models;
using CRMSystem.Core.ProjectionModels.Bill;
using CRMSystem.Core.ProjectionModels.Car;
using CRMSystem.Core.ProjectionModels.Client;
using CRMSystem.Core.ProjectionModels.Expense;
using CRMSystem.Core.ProjectionModels.Guarantee;
using CRMSystem.Core.ProjectionModels.Notification;
using CRMSystem.Core.ProjectionModels.Order;
using CRMSystem.Core.ProjectionModels.Part;
using CRMSystem.Core.ProjectionModels.PartCategory;
using CRMSystem.Core.ProjectionModels.PartSet;
using CRMSystem.Core.ProjectionModels.PaymentNote;
using CRMSystem.Core.ProjectionModels.Position;
using CRMSystem.Core.ProjectionModels.Schedule;
using CRMSystem.Core.ProjectionModels.Shift;
using CRMSystem.Core.ProjectionModels.Specialization;
using CRMSystem.Core.ProjectionModels.StorageCell;
using CRMSystem.Core.ProjectionModels.Supplier;
using CRMSystem.Core.ProjectionModels.Supply;
using CRMSystem.Core.ProjectionModels.SupplySet;
using CRMSystem.Core.ProjectionModels.Tax;
using CRMSystem.Core.ProjectionModels.User;
using CRMSystem.Core.ProjectionModels.Worker;
using CRMSystem.Core.ProjectionModels.WorkInOrder;
using FluentAssertions;
using Shared.Enums;

namespace CRMSystem.Business.Tests;

internal static class ValidObjects
{
    internal static NotificationCreateModel CreateValidNotification()
    {
        var notification = new NotificationCreateModel(
            1,
            2,
            NotificationTypeEnum.Client,
            NotificationStatusEnum.Sent,
            "Test",
            new DateTime(2025, 1, 1));

        notification.Should().NotBeNull();

        return notification;
    }

    internal static Absence CreateValidAbsence(DateOnly? endDate)
    {
        var (absence, errors) = Absence.Create(
                        0,
                        1,
                        AbsenceTypeEnum.Vacation,
                        new DateOnly(2025, 1, 1),
                        endDate);

        absence.Should().NotBeNull();
        errors.Should().BeEmpty();

        return absence;
    }

    internal static BillCreateModel CreateValidBill()
    {
        var bill = new BillCreateModel(
            1,
            BillStatusEnum.Unpaid,
            new DateTime(2025, 1, 1),
            0,
            null);

        bill.Should().NotBeNull();

        return bill;
    }

    internal static CarCreateModel CreateValidCar(CarStatusEnum status)
    {
        var car = new CarCreateModel(
            1,
            status,
            "Test",
            "Test",
            2008,
            "VF3MJAHXVHS101043",
            "1111AA-1",
            15000);

        car.Should().NotBeNull();

        return car;
    }

    internal static UserCreateModel CreateValidUserClient()
    {
        var user = new UserCreateModel(
            (int)RoleEnum.Client,
            "ClientUser",
            "TestTestTest");

        user.Should().NotBeNull();

        return user;
    }

    internal static UserCreateModel CreateValidWorkerUser()
    {
        var user = new UserCreateModel(
            (int)RoleEnum.Worker,
            "ClientUser",
            "TestTestTest");

        user.Should().NotBeNull();

        return user;
    }

    internal static ClientCreateModel CreateValidClient(long userId)
    {
        var client = new ClientCreateModel(
            userId,
            "TestTest",
            "TestTestTest",
            "80444444444",
            "TEstTestTest");

        client.Should().NotBeNull();

        return client;
    }

    internal static ExpenseCreateModel CreateValidExpense(int? taxId, long? partSetId)
    {
        var expense = new ExpenseCreateModel(
            new DateTime(2025, 1, 1),
            "Test",
            taxId,
            partSetId,
            ExpenseTypeEnum.FinancialCharges,
            123);

        expense.Should().NotBeNull();

        return expense;
    }

    internal static GuaranteeCreateModel CreateValidGuarantee()
    {
        var guarantee = new GuaranteeCreateModel(
            1,
            new DateOnly(2025, 1, 1),
            new DateOnly(2026, 1, 1),
            null,
            "Test");

        guarantee.Should().NotBeNull();

        return guarantee;
    }

    internal static OrderCreateModel CreateValidOrder()
    {
        var order = new OrderCreateModel(
            OrderStatusEnum.Pending,       
            1,                             
            DateOnly.FromDateTime(DateTime.Now), 
            null,                        
            null,                   
            OrderPriorityEnum.Medium);
        
        order.Should().NotBeNull();

        return order;
    }

    internal static PartCategoryCreateModel CreateValidPartCategory()
    {
        var category = new PartCategoryCreateModel(
            "Test",
            "Test");
        
        category.Should().NotBeNull();

        return category;
    }

    internal static PartCreateModel CreateValidPart()
    {
        var part = new PartCreateModel(
            1,
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            "Test");

        part.Should().NotBeNull();

        return part;
    }

    internal static PartSetCreateModel CreateValidPartSet(long? orderId, long? proposalId)
    {
        var partSet = new PartSetCreateModel(
            orderId,
            1,
            proposalId,
            1,
            1);

        partSet.Should().NotBeNull();

        return partSet;
    }

    internal static PaymentNoteCreateModel CreateValidPaymentNote()
    {
        var note = new PaymentNoteCreateModel(
            1,
            new DateTime(2025, 1, 1),
            100,
            PaymentMethodEnum.Cash);

        note.Should().NotBeNull();

        return note;
    }
    
    internal static PositionCreateModel CreateValidPosition()
    {
        var position = new PositionCreateModel(
            1,
            2,
            50,
            100,
            3);

        position.Should().NotBeNull();

        return position;
    }

    internal static ScheduleCreateModel CreateValidSchedule()
    {
        var schedule = new ScheduleCreateModel(
            1,
            2,
            new DateTime(2025, 1, 1));

        schedule.Should().NotBeNull();

        return schedule;
    }

    internal static ShiftCreateModel CreateValidShift()
    {
        var shift = new ShiftCreateModel(
            "Test",
            new TimeOnly(6, 30),
            new TimeOnly(18, 30));

        shift.Should().NotBeNull();

        return shift;
    }

    internal static Skill CreateValidSkill()
    {
        var (skill, errors) = Skill.Create(
            0,
            1,
            2);

        skill.Should().NotBeNull();
        errors.Should().BeEmpty();

        return skill;
    }

    internal static SpecializationCreateModel CreateValidSpecialization()
    {
        var spec = new SpecializationCreateModel(
            "Test");

        spec.Should().NotBeNull();

        return spec;
    }

    internal static StorageCellCreateModel CreateValidStorageCell()
    {
        var cell = new StorageCellCreateModel(
            "Test",
            "Test");

        cell.Should().NotBeNull();

        return cell;
    }

    internal static SupplierCreateModel CreateValidSupplier()
    {
        var supplier = new SupplierCreateModel(
            "Test",
            "Test");

        supplier.Should().NotBeNull();

        return supplier;
    }

    internal static SupplyCreateModel CreateValidSupply()
    {
        var supply = new SupplyCreateModel(
            1,
            new DateOnly(2025, 1, 1));

        supply.Should().NotBeNull();

        return supply;
    }

    internal static SupplySetCreateModel CreateValidSupplySet()
    {
        var set = new SupplySetCreateModel(
            1,
            2,
            3,
            4);

        set.Should().NotBeNull();

        return set;
    }

    internal static TaxCreateModel CreateValidTax()
    {
        var tax = new TaxCreateModel(
            "Test",
            1,
            TaxTypeEnum.LocalFees);

        tax.Should().NotBeNull();

        return tax;
    }

    internal static WorkerCreateModel CreateValidWorker()
    {
        var worker = new WorkerCreateModel(
            1,
            "Test",
            "Test",
            2,
            "80445555555",
            "Test");

        worker.Should().NotBeNull();

        return worker;
    }

    internal static WorkInOrderCreateModel CreateValidWorkInOrder()
    {
        var wio = new WorkInOrderCreateModel(
            1,
            2,
            3,
            WorkStatusEnum.InProgress,
            4);

        wio.Should().NotBeNull();

        return wio;
    }
}
