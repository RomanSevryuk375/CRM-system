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

        builder.Property(b => b.Id)
            .HasColumnName("bill_id")
            .IsRequired();

        builder.Property(b => b.OrderId)
            .HasColumnName("bill_order_id")
            .IsRequired();

        builder.Property(b => b.StatusId)
            .HasColumnName("bill_status_id")
            .IsRequired();

        builder.Property(b => b.Date)
            .HasColumnName("bill_date")
            .IsRequired();

        builder.Property(b => b.Amount)
            .HasColumnName("bill_total_sum")
            .IsRequired();

        builder.Property(b => b.ActualBillDate)
            .HasColumnName("actual_closing_bill_date")
            .IsRequired(false);

        builder.HasOne(s => s.Status)
            .WithMany(b => b.Bills)
            .HasForeignKey(b => b.StatusId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Order)
            .WithMany(b => b.Bills)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
