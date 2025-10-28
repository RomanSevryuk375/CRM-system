using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class PaymentNoteConfiguration : IEntityTypeConfiguration<PaymentNoteEntity>
{
    void IEntityTypeConfiguration<PaymentNoteEntity>.Configure(EntityTypeBuilder<PaymentNoteEntity> builder)
    {
        builder.ToTable("payment_journal");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasColumnName("payment_id")
            .IsRequired();

        builder.Property(p => p.BillId)
            .HasColumnName("payment_bill_id")
            .IsRequired();

        builder.Property(p => p.Date)
            .HasColumnName("payment_date")
            .IsRequired();

        builder.Property(p => p.Amount)
            .HasColumnName("payment_amount")
            .IsRequired();

        builder.Property(p => p.Method)
            .HasColumnName("payment_method")
            .IsRequired();

        builder.HasOne(p => p.Bill)
            .WithMany(b => b.Payments)
            .HasForeignKey(p => p.BillId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
