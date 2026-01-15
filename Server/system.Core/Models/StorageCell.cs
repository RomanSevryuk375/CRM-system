using CRMSystem.Core.Constants;
using CRMSystem.Core.Validation;

namespace CRMSystem.Core.Models;

public class StorageCell
{
    private StorageCell(int id, string rack, string shelf)
    {
        Id = id;
        Rack = rack;
        Shelf = shelf;
    }
    public int Id { get; }
    public string Rack { get; }
    public string Shelf { get; }

    public static (StorageCell? storageCell, List<string> errors) Create(int id, string rack, string shelf)
    {
        var errors = new List<string>();

        var idError = DomainValidator.ValidateId(id, "id");
        if (!string.IsNullOrEmpty(idError)) errors.Add(idError);

        var rackError = DomainValidator.ValidateString(rack, ValidationConstants.MAX_STORAGE_ITEM_LENGTH, "rack");
        if (!string.IsNullOrEmpty(rackError)) errors.Add(rackError);

        var shelfError = DomainValidator.ValidateString(shelf, ValidationConstants.MAX_STORAGE_ITEM_LENGTH, "shelf");
        if (!string.IsNullOrEmpty(shelfError)) errors.Add(shelfError);

        if(errors.Any())
            return (null, errors);

        var storageCell = new StorageCell(id, rack, shelf);

        return (storageCell, new List<string>());

    }
}
