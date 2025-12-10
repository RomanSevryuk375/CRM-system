using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class BillConfiguration : IEntityTypeConfiguration<BillEntity>
{
    void IEntityTypeConfiguration<BillEntity>.Configure(EntityTypeBuilder<BillEntity> builder)
    {

        builder.ToTable("bills");

        builder.HasKey(x => x.Id);

        builder.Property(b => b.OrderId)
            .IsRequired();

        builder.Property(b => b.StatusId)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
            .IsRequired();

        builder.Property(b => b.Amount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(b => b.ActualBillDate)
            .IsRequired(false);

        builder.Ignore(b => b.LastBillDate);

        builder.HasOne(s => s.Status)
            .WithMany(b => b.Bills)
            .HasForeignKey(b => b.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Order)
            .WithMany(b => b.Bills)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
