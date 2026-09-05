using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Abstractions.Results;

namespace CRM.Inventory.Domain.Entities;

public sealed class PartCategory : Entity<PartCategoryId>
{
    private PartCategory(PartCategoryId id, Name name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

#pragma warning disable CS8618
    private PartCategory() { }
#pragma warning restore CS8618

    public Name Name { get; private set; }
    public string? Description { get; private set; }

    public static Result<PartCategory> Create(PartCategoryId id, Name name, string? description)
    {
        return Result<PartCategory>.Success(new PartCategory(id, name, description));
    }
}
