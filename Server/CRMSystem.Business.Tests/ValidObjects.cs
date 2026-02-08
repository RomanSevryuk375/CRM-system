using CRMSystem.Core.Models;
using FluentAssertions;
using Shared.Enums;
using Order = CRMSystem.Core.Models.Order;

namespace CRMSystem.Business.Tests;

internal static class ValidObjects
{
    internal static Notification CreateValidNotification()
    {
        var (notification, errors) = Notification.Create(
            0,
            1,
            2,
            NotificationTypeEnum.Client,
            NotificationStatusEnum.Sent,
            "Test",
            new DateTime(2025, 1, 1));

        notification.Should().NotBeNull();
        errors.Should().BeEmpty();

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

    internal static Bill CreateValidBill()
    {
        var (bill, errors) = Bill.Create(
            0,
            1,
            BillStatusEnum.Unpaid,
            new DateTime(2025, 1, 1),
            0,
            null);

        bill.Should().NotBeNull();
        errors.Should().BeEmpty();

        return bill;
    }

    internal static Car CreateValidCar(CarStatusEnum status)
    {
        var (car, errors) = Car.Create(
            0,
            1,
            status,
            "Test",
            "Test",
            2008,
            "VF3MJAHXVHS101043",
            "1111AA-1",
            15000);

        car.Should().NotBeNull();
        errors.Should().BeEmpty();

        return car;
    }

    internal static User CreateValidUserClient()
    {
        var (user, errorsUser) = User.Create(
            0,
            (int)RoleEnum.Client,
            "ClientUser",
            "TestTestTest");

        user.Should().NotBeNull();
        errorsUser.Should().BeEmpty();

        return user;
    }

    internal static User CreateValidWorkerUser()
    {
        var (user, errorsUser) = User.Create(
            0,
            (int)RoleEnum.Worker,
            "ClientUser",
            "TestTestTest");

        user.Should().NotBeNull();
        errorsUser.Should().BeEmpty();

        return user;
    }

    internal static Client CreateValidClient(long UserId)
    {
        var (client, errorsClient) = Client.Create(
            0,
            UserId,
            "TestTest",
            "TestTestTest",
            "80444444444",
            "TEstTestTest");

        client.Should().NotBeNull();
        errorsClient.Should().BeEmpty();

        return client;
    }

    internal static Expense CreateValidExpense(int? taxId, long? partSetId)
    {
        var (expense, errors) = Expense.Create(
            0,
            new DateTime(2025, 1, 1),
            "Test",
            taxId,
            partSetId,
            ExpenseTypeEnum.FinancialCharges,
            123);

        expense.Should().NotBeNull();
        errors.Should().BeEmpty();

        return expense;
    }

    internal static Guarantee CreateValidGuarantee()
    {
        var (guarantee, errors) = Guarantee.Create(
            0,
            1,
            new DateOnly(2025, 1, 1),
            new DateOnly(2026, 1, 1),
            null,
            "Test");

        guarantee.Should().NotBeNull();
        errors.Should().BeEmpty();

        return guarantee;
    }

    internal static Order CreateValidOrder()
    {
        var (order, errors) = Order.Create(
                        123,
                        OrderStatusEnum.Accepted,
                        123,
                        new DateOnly(2025, 1, 1),
                        OrderPriorityEnum.Medium);

        errors.Should().BeEmpty();
        order.Should().NotBeNull();

        return order;
    }

    internal static PartCategory CreateValidPartCategory()
    {
        var (category, errors) = PartCategory.Create(
            0,
            "Test",
            "Test");

        errors.Should().BeEmpty();
        category.Should().NotBeNull();

        return category;
    }

    internal static Part CreateValidPart()
    {
        var (part, errors) = Part.Create(
            0,
            1,
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            "Test",
            "Test");

        part.Should().NotBeNull();
        errors.Should().BeEmpty();

        return part;
    }

    internal static PartSet CreateValidPartSet(long? orderId, long? proposalId)
    {
        var (partSet, errors) = PartSet.Create(
            0,
            orderId,
            1,
            proposalId,
            1,
            1);

        partSet.Should().NotBeNull();
        errors.Should().BeEmpty();

        return partSet;
    }

    internal static PaymentNote CreateValidPaymentNote()
    {
        var (note, errors) = PaymentNote.Create(
            0,
            1,
            new DateTime(2025, 1, 1),
            100,
            PaymentMethodEnum.Cash);

        note.Should().NotBeNull();
        errors.Should().BeEmpty();

        return note;
    }

    internal static Position CreateValidPosition()
    {
        var (position, errors) = Position.Create(
            0,
            1,
            2,
            50,
            100,
            3);

        position.Should().NotBeNull();
        errors.Should().BeEmpty();

        return position;
    }

    internal static Schedule CreateValidSchedul()
    {
        var (schedule, errors) = Schedule.Create(
            0,
            1,
            2,
            new DateTime(2025, 1, 1));

        schedule.Should().NotBeNull();
        errors.Should().BeEmpty();

        return schedule;
    }

    internal static Shift CreateValidShift()
    {
        var (shift, errors) = Shift.Create(
            0,
            "Test",
            new TimeOnly(6, 30),
            new TimeOnly(18, 30));

        shift.Should().NotBeNull();
        errors.Should().BeEmpty();

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

    internal static Specialization CreateValidSpecialization()
    {
        var (spec, error) = Specialization.Create(
            1,
            "Test");

        spec.Should().NotBeNull();
        error.Should().BeEmpty();

        return spec;
    }

    internal static StorageCell CreateValidStorageCell()
    {
        var (cell, errors) = StorageCell.Create(
            1,
            "Test",
            "Test");

        cell.Should().NotBeNull();
        errors.Should().BeEmpty();

        return cell;
    }

    internal static Supplier CreateValidSupplier()
    {
        var (supplier, errors) = Supplier.Create(
            1,
            "Test",
            "Test");

        supplier.Should().NotBeNull();
        errors.Should().BeEmpty();

        return supplier;
    }

    internal static Supply CreateValidSupply()
    {
        var (supply, errors) = Supply.Create(
            0,
            1,
            new DateOnly(2025, 1, 1));

        supply.Should().NotBeNull();
        errors.Should().BeEmpty();

        return supply;
    }

    internal static SupplySet CreateValidSupplySet()
    {
        var (set, errors) = SupplySet.Create(
            0,
            1,
            2,
            3,
            4);

        set.Should().NotBeNull();
        errors.Should().BeEmpty();

        return set;
    }

    internal static Tax CreateValidTax()
    {
        var (tax, errors) = Tax.Create(
            0,
            "Test",
            1,
            TaxTypeEnum.LocalFees);

        tax.Should().NotBeNull();
        errors.Should().BeEmpty();

        return tax;
    }

    internal static Worker CreateValidWorker()
    {
        var (worker, errors) = Worker.Create(
            0,
            1,
            "Test",
            "Test",
            2,
            "80445555555",
            "Test");

        worker.Should().NotBeNull();
        errors.Should().BeEmpty();

        return worker;
    }

    internal static WorkInOrder CreateValidWorkInOrder()
    {
        var (wio, errors) = WorkInOrder.Create(
            0,
            1,
            2,
            3,
            WorkStatusEnum.InProgress,
            4);

        wio.Should().NotBeNull();
        errors.Should().BeEmpty();

        return wio;
    }
}
