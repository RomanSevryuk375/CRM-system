using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Ordering.Domain.Entities.Orders;

public sealed class OrderGuarantee : IEntity<OrderGuaranteeId>
{
    public const int MaxDescriptionLength = 2000;
    public const int MaxTermsLength = 2000;

    internal OrderGuarantee(
        OrderGuaranteeId id,
        OrderId orderId,
        OrderPartId? orderPartId,
        OrderWorkId? orderWorkId,
        DateOnly dateStart,
        DateOnly dateEnd,
        string? description,
        string terms)
    {
        Id = id;
        OrderId = orderId;
        OrderPartId = orderPartId;
        OrderWorkId = orderWorkId;
        DateStart = dateStart;
        DateEnd = dateEnd;
        Description = description;
        Terms = terms;
    }

#pragma warning disable CS8618
    private OrderGuarantee() { }
#pragma warning restore CS8618

    public OrderGuaranteeId Id { get; private set; }
    public OrderId OrderId { get; private set; }

    public OrderPartId? OrderPartId { get; private set; }
    public OrderWorkId? OrderWorkId { get; private set; }

    public DateOnly DateStart { get; private set; }
    public DateOnly DateEnd { get; private set; }

    public string? Description { get; private set; }
    public string Terms { get; private set; }

    internal static Result<OrderGuarantee> Create(
        OrderGuaranteeId id,
        OrderId orderId,
        OrderPartId? orderPartId,
        OrderWorkId? orderWorkId,
        DateOnly dateStart,
        DateOnly dateEnd,
        string? description,
        string terms)
    {
        List<Error> errors = [];

        if (dateStart >= dateEnd)
        {
            errors.Add(Error.Validation<OrderGuarantee>(Errors.InvalidDateRange));
        }

        if (string.IsNullOrWhiteSpace(terms))
        {
            errors.Add(Error.Validation<OrderGuarantee>(Errors.TermsEmpty));
        }
        else if (terms.Length > MaxTermsLength)
        {
            errors.Add(Error.Validation<OrderGuarantee>(Errors.TermsTooLong));
        }

        if (description?.Length > MaxDescriptionLength)
        {
            errors.Add(Error.Validation<OrderGuarantee>(Errors.DescriptionTooLong));
        }

        if (orderPartId is not null && orderWorkId is not null)
        {
            errors.Add(Error.Validation<OrderGuarantee>(Errors.BothPartAndWorkSpecified));
        }

        if (errors.Count != 0)
        {
            return Result<OrderGuarantee>.Failure(Error.Validation<OrderGuarantee>(
                string.Join("; ", errors.Select(x => x.Message))));
        }

        OrderGuarantee guarantee = new(
            id,
            orderId,
            orderPartId,
            orderWorkId,
            dateStart,
            dateEnd,
            description,
            terms);

        return Result<OrderGuarantee>.Success(guarantee);
    }

    public static class Errors
    {
        public const string InvalidDateRange = "Start date must be strictly earlier than end date.";
        public const string TermsEmpty = "Guarantee terms cannot be empty.";
        public static readonly string TermsTooLong = $"Guarantee terms exceed {MaxTermsLength} characters.";
        public static readonly string DescriptionTooLong = $"Description exceeds {MaxDescriptionLength} characters.";
        public const string BothPartAndWorkSpecified = "A guarantee cannot be linked to both a specific part and a specific work simultaneously.";
    }
}