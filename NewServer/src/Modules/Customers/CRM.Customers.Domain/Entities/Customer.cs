using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Customers.Domain.Entities;

public sealed class Customer : AggregateRoot<CustomerId>, IAuditable, ISoftDeletable
{
    private Customer(
        CustomerId id,
        UserId userId,
        Name name,
        Name surname,
        PhoneNumber phoneNumber,
        Email email)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Surname = surname;
        PhoneNumber = phoneNumber;
        Email = email;
    }

#pragma warning disable CS8618
    private Customer() { }
#pragma warning restore CS8618

    public UserId UserId { get; private set; }
    public Name Name { get; private set; }
    public Name Surname { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }

#pragma warning disable S1144
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public Guid CreatedBy { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    public Guid? UpdatedBy { get; private set; }
#pragma warning restore S1144

    public static Result<Customer> Create(
        CustomerId id,
        UserId userId,
        Name name,
        Name surname,
        PhoneNumber phoneNumber,
        Email email)
    {
        Customer customer = new(id, userId, name, surname, phoneNumber, email);
        customer.IncrementVersion();

        return Result<Customer>.Success(customer);
    }

    public Result UpdateContactInfo(PhoneNumber phoneNumber, Email email)
    {
        PhoneNumber = phoneNumber;
        Email = email;
        IncrementVersion();

        return Result.Success();
    }
}
