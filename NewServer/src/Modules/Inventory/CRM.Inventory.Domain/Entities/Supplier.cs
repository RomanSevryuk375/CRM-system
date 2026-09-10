using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class Supplier : AggregateRoot<SupplierId>, IAuditable, ISoftDeletable
{
    public const int MaxContactsLength = 500;

    private Supplier(SupplierId id, Name name, string contacts)
    {
        Id = id;
        Name = name;
        Contacts = contacts;
    }

#pragma warning disable CS8618
    private Supplier() { }
#pragma warning restore CS8618

    public Name Name { get; private set; }
    public string Contacts { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Supplier> Create(SupplierId id, Name name, string contacts)
    {
        if (string.IsNullOrWhiteSpace(contacts))
        {
            return Result<Supplier>.Failure(Error.Validation<Supplier>(Errors.ContactsEmpty));
        }

        if (contacts.Length > MaxContactsLength)
        {
            return Result<Supplier>.Failure(Error.Validation<Supplier>(Errors.ContactsTooLong));
        }

        Supplier supplier = new(id, name, contacts);
        supplier.IncrementVersion();

        return Result<Supplier>.Success(supplier);
    }

    public static class Errors
    {
        public const string ContactsEmpty = "Contacts cannot be empty.";
        public static readonly string ContactsTooLong = $"Contacts exceed maximum length of {MaxContactsLength} characters.";
    }
}
