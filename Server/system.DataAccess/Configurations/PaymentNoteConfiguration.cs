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

        builder.Property(p => p.BillId)
            .IsRequired();

        builder.Property(p => p.Date)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                )
            .IsRequired();

        builder.Property(p => p.Amount)
            .HasColumnType("decimal(18, 2)")
            .IsRequired();

        builder.Property(p => p.Method)
            .IsRequired();

        builder.HasOne(p => p.Bill)
            .WithMany(b => b.Payments)
            .HasForeignKey(p => p.BillId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
