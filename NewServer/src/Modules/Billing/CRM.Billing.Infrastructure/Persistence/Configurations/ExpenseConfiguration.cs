using CRM.Billing.Domain.Entities;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Billing.Infrastructure.Persistence.Configurations;

internal sealed class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        builder.ToTable("expenses", BillingDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Amount).IsRequired();
        builder.Property(x => x.TaxId).IsRequired(false);
        
        builder.Property(x => x.Category)
            .HasMaxLength(Expense.MaxCategoryLength)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(Expense.MaxDescriptionLength)
            .IsRequired(false);

        builder.Property(x => x.ReferenceId).IsRequired(false);

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.TaxId);
        builder.HasIndex(x => x.ReferenceId);
    }
}
