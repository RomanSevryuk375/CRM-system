using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Shared.Infrastructure.Data.Extensions;

public static class EntityTypeBuilderExtensions
{
    public static EntityTypeBuilder<T> ConfigureSoftDelete<T>(this EntityTypeBuilder<T> builder)
        where T : class, ISoftDeletable
    {
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.HasQueryFilter(x => !x.IsDeleted);

        return builder;
    }

    public static EntityTypeBuilder<T> ConfigureAudit<T>(this EntityTypeBuilder<T> builder)
        where T : class, IAuditable
    {
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        return builder;
    }

    public static EntityTypeBuilder<T> ConfigureConcurrency<T>(this EntityTypeBuilder<T> builder)
        where T : class, IHasVersion
    {
        builder.Property(x => x.Version).IsConcurrencyToken();

        return builder;
    }

    public static EntityTypeBuilder<T> ConfigureBaseEntity<T>(this EntityTypeBuilder<T> builder)
        where T : class, IAuditable, ISoftDeletable, IHasVersion
    {
        builder.ConfigureSoftDelete();
        builder.ConfigureAudit();
        builder.ConfigureConcurrency();

        return builder;
    }
}
