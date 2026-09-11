using System;
using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;

namespace CRM.TestHelpers;

public static class BillMother
{
    public static readonly DateOnly Today = new(2026, 9, 11);

    public static Bill CreateUnpaid(
        BillId? id = null,
        OrderId? orderId = null,
        decimal amount = 1000m)
    {
        return Bill.Create(
            id ?? new BillId(Guid.NewGuid()), 
            orderId ?? new OrderId(Guid.NewGuid()), 
            BillStatus.Unpaid, 
            amount, 
            null, 
            Today).Value;
    }

    public static Bill CreatePaid(
        BillId? id = null,
        OrderId? orderId = null,
        decimal amount = 1000m,
        DateOnly? actualDate = null)
    {
        var date = actualDate ?? Today;
        return Bill.Create(
            id ?? new BillId(Guid.NewGuid()), 
            orderId ?? new OrderId(Guid.NewGuid()), 
            BillStatus.Paid, 
            amount, 
            date, 
            Today).Value;
    }
}
