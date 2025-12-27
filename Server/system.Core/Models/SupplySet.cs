using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class SupplySet
{
    public SupplySet(long id, long supplyId, long positionId, decimal quantity, decimal purchasePrice)
    {
        Id = id;
        SupplyId = supplyId;
        PositionId = positionId;
        Quantity = quantity;
        PurchasePrice = purchasePrice;
    }

    public long Id { get; }
    public long SupplyId { get; }
    public long PositionId { get; }
    public decimal Quantity { get; }
    public decimal PurchasePrice { get; }

    public static (SupplySet? supplySet, List<string> errors) Create(long id, long supplyId, long positionId, decimal quantity, decimal purchasePrice)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var supplyError = DomainValidator.ValidateId(supplyId, "supplyId");
        if (!string.IsNullOrEmpty(supplyError)) errors.Add(supplyError);

        var positionIdError = DomainValidator.ValidateId(positionId, "positionId");
        if (!string.IsNullOrEmpty(positionIdError)) errors.Add(positionIdError);

        var quantityError = DomainValidator.ValidateMoney(quantity, "quantity");
        if (!string.IsNullOrEmpty(quantityError)) errors.Add(quantityError);

        var priceError = DomainValidator.ValidateMoney(purchasePrice, "price");
        if (!string.IsNullOrEmpty(priceError)) errors.Add(priceError);

        if (errors.Any())
            return (null, errors);

        var supplySet = new SupplySet(id, supplyId, positionId, quantity, purchasePrice);

        return (supplySet, new List<string>());
    }
}
