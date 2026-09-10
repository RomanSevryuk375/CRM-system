using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class StorageCell : Entity<StorageCellId>
{
    public const int MaxRackLength = 50;
    public const int MaxShelfLength = 50;

    private StorageCell(StorageCellId id, string rack, string shelf)
    {
        Id = id;
        Rack = rack;
        Shelf = shelf;
    }

#pragma warning disable CS8618
    private StorageCell() { }
#pragma warning restore CS8618

    public string Rack { get; private set; }
    public string Shelf { get; private set; }

    public static Result<StorageCell> Create(StorageCellId id, string rack, string shelf)
    {
        if (string.IsNullOrWhiteSpace(rack))
        {
            return Result<StorageCell>.Failure(Error.Validation<StorageCell>(Errors.RackEmpty));
        }

        if (rack.Length > MaxRackLength)
        {
            return Result<StorageCell>.Failure(Error.Validation<StorageCell>(Errors.RackTooLong));
        }

        if (string.IsNullOrWhiteSpace(shelf))
        {
            return Result<StorageCell>.Failure(Error.Validation<StorageCell>(Errors.ShelfEmpty));
        }

        if (shelf.Length > MaxShelfLength)
        {
            return Result<StorageCell>.Failure(Error.Validation<StorageCell>(Errors.ShelfTooLong));
        }

        return Result<StorageCell>.Success(new StorageCell(id, rack, shelf));
    }

    public static class Errors
    {
        public const string RackEmpty = "Rack cannot be empty.";
        public static readonly string RackTooLong = $"Rack exceeds maximum length of {MaxRackLength} characters.";
        public const string ShelfEmpty = "Shelf cannot be empty.";
        public static readonly string ShelfTooLong = $"Shelf exceeds maximum length of {MaxShelfLength} characters.";
    }
}
