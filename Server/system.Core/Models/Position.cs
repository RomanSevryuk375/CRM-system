using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class Position
{
    public Position(long id, long partId, int cellId, decimal purchasePrice, decimal sellingPrice, decimal quantity)
    {
        Id = id;
        PartId = partId;
        CellId = cellId;
        PurchasePrice = purchasePrice;
        SellingPrice = sellingPrice;
        Quantity = quantity;
    }

    public long Id { get; }
    public long PartId { get; }
    public int CellId { get; }
    public decimal PurchasePrice { get; }
    public decimal SellingPrice { get; }
    public decimal Quantity { get; }

    public static (Position? position, List<string> errors) Create(long id, long partId, int cellId, decimal purchasePrice, decimal sellingPrice, decimal quantity)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var partIdError = DomainValidator.ValidateId(partId, "partId");
        if (!string.IsNullOrEmpty(partIdError)) errors.Add(partIdError);

        var cellIdError = DomainValidator.ValidateId(cellId, "cellId");
        if (!string.IsNullOrEmpty(cellIdError)) errors.Add(cellIdError);

        var purchaseError = DomainValidator.ValidateMoney(purchasePrice, "purchasePrice");
        if (!string.IsNullOrEmpty(purchaseError)) errors.Add(purchaseError);

        var sellingError = DomainValidator.ValidateMoney(sellingPrice, "sellingPrice");
        if (!string.IsNullOrEmpty(sellingError)) errors.Add(sellingError);

        var quantityError = DomainValidator.ValidateMoney(quantity, "quantity");
        if (!string.IsNullOrEmpty(quantityError)) errors.Add(quantityError);

        if (errors.Any())
            return(null, errors);

        var position = new Position(id, partId, cellId, purchasePrice, sellingPrice, quantity);

        return (position, new List<string>());
    }
}
