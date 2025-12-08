using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<ExpenseEntity>
{
    void IEntityTypeConfiguration<ExpenseEntity>.Configure(EntityTypeBuilder<ExpenseEntity> builder)
    {

        builder.ToTable("expenses");

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Date)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
            .IsRequired();

        builder.Property(e => e.Category)
            .IsRequired();

        builder.Property(e => e.TaxId)
            .IsRequired(false);

        builder.Property(e => e.PartSetId)
            .IsRequired(false);

        builder.Property(e => e.ExpenseType)
            .IsRequired();

        builder.Property(e => e.Sum)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.HasOne(t => t.Tax)
            .WithMany(e => e.Expenses)
            .HasForeignKey(e => e.TaxId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(up => up.PartSet)
            .WithMany(e => e.Expenses)
            .HasForeignKey(e => e.PartSet)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
