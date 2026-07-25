using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("expenses", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new ExpenseId(value))
            .IsRequired();

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Amount)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.TaxId)
            .HasConversion<Guid?>(
                taxId => taxId.HasValue
                    ? taxId.Value.Id
                    : null,
                dbValue => dbValue.HasValue
                    ? new TaxId(dbValue.Value)
                    : null)
            .IsRequired(false);

        builder.Property(x => x.Category)
            .HasMaxLength(Expense.MaxCategoryLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Expense.MaxDescriptionLength)
            .IsRequired(false);
        builder.Property(x => x.ReferenceId).IsRequired(false);

        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.TaxId);
        builder.HasIndex(x => x.ReferenceId);
    }
}
