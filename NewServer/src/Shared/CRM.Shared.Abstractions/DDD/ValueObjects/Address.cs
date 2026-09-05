namespace CRM.Shared.Abstractions.DDD.ValueObjects;

using CRM.Shared.Abstractions.Results;

public sealed class Address : ValueObject
{
    private Address(string street, string city, string? state, string? zipCode, string? country)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
    }

    public string Street { get; }
    public string City { get; }
    public string? State { get; }
    public string? ZipCode { get; }
    public string? Country { get; }

    public static Result<Address> Create(
        string? street, 
        string? city, 
        string? state, 
        string? zipCode, 
        string? country)
    {
        if (string.IsNullOrWhiteSpace(street))
        {
            return Result<Address>.Failure(Error.Validation<Address>(Errors.StreetEmpty));
        }

        if (string.IsNullOrWhiteSpace(city))
        {
            return Result<Address>.Failure(Error.Validation<Address>(Errors.CityEmpty));
        }

        return Result<Address>.Success(
            new Address(street, city, state, zipCode, country));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        
        if (State != null)
        {
            yield return State;
        }

        if (ZipCode != null)
        {
            yield return ZipCode;
        }

        if (Country != null)
        {
            yield return Country;
        }

    }

    public static class Errors
    {
        public const string StreetEmpty = "Street cannot be empty.";
        public const string CityEmpty = "City cannot be empty.";
    }
}
