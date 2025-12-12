using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class PartSet
{
    public PartSet(long id, long? orderId, long positionId, long? proposalId, decimal quantity, decimal soldPrice)
    {
        Id = id;
        OrderId = orderId;
        PositionId = positionId;
        ProposalId = proposalId;
        Quantity = quantity;
        SoldPrice = soldPrice;
    }
    public long Id { get; }
    public long? OrderId { get; }
    public long PositionId { get; }
    public long? ProposalId { get; }
    public decimal Quantity { get; }
    public decimal SoldPrice { get; }

    public static (PartSet? partSet, List<string> errors) Create(long id, long? orderId, long positionId, long? proposalId, decimal quantity, decimal soldPrice)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var positionError = DomainValidator.ValidateId(positionId, "position");
        if (!string.IsNullOrEmpty(positionError)) errors.Add(positionError);

        var quantityError = DomainValidator.ValidateMoney(quantity, "quantity");
        if (!string.IsNullOrEmpty(quantityError)) errors.Add(quantityError);

        var soldPriceError = DomainValidator.ValidateMoney(soldPrice, "soldPrice");
        if (!string.IsNullOrEmpty(soldPriceError)) errors.Add(soldPriceError);

        if (errors.Any())
            return (null, errors);

        var partSet = new PartSet(id, orderId, positionId, proposalId, quantity, soldPrice);

        return (partSet, new List<string>());
    }
}
