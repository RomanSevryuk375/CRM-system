using System;
using CRM.Billing.Domain.Entities;
using CRM.Billing.Domain.Enums;
using CRM.Shared.Abstractions.Abstractions;

namespace CRM.TestHelpers;

public static class ExpenseMother
{
    public static readonly DateOnly Today = new(2026, 9, 11);

    public static Expense CreateDefault(
        ExpenseId? id = null,
        DateOnly? date = null,
        string category = "Office supplies",
        string? description = "Paper and pens",
        ExpenseType type = ExpenseType.OfficeAndSupplies,
        decimal amount = 100m,
        DateOnly? today = null)
    {
        var dateVal = date ?? Today;
        var todayVal = today ?? Today;
        return Expense.Create(
            id ?? new ExpenseId(Guid.NewGuid()),
            dateVal,
            category,
            description,
            type,
            amount,
            todayVal).Value;
    }
}
