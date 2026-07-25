namespace CRM.Shared.Abstractions.Abstractions;

public readonly record struct UserId(Guid Id);
public readonly record struct BillId(Guid Id);
public readonly record struct OrderId(Guid Id);
public readonly record struct PaymentNoteId(Guid Id);
public readonly record struct TaxId(Guid Id);
public readonly record struct ExpenseId(Guid Id);
public readonly record struct PriceListId(Guid Id);
public readonly record struct PriceListItemId(Guid Id);
public readonly record struct JobId(Guid Id);
public readonly record struct CarId(Guid Id);
public readonly record struct WorkerId(Guid Id);
public readonly record struct OrderWorkId(Guid Id);
public readonly record struct OrderPartId(Guid Id);
public readonly record struct PartId(Guid Id);
public readonly record struct OrderGuaranteeId(Guid Id);
