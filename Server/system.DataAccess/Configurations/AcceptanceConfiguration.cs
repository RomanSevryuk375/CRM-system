using CRMSystem.Core.Constants;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

internal class AcceptanceConfiguration : IEntityTypeConfiguration<AcceptanceEntity>
{
    void IEntityTypeConfiguration<AcceptanceEntity>.Configure(EntityTypeBuilder<AcceptanceEntity> builder)
    {

        builder.ToTable("acceptances");

        builder.HasKey(x => x.Id);

        builder.Property(a => a.OrderId)
            .IsRequired();

        builder.Property(a => a.WorkerId)
            .IsRequired();

        builder.Property(a => a.CreateAt)
            .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            )
            .IsRequired();

        builder.Property(a => a.Mileage)
            .IsRequired();

        builder.Property(a => a.FuelLevel)
            .IsRequired();

        builder.Property(a => a.ExternalDefects) 
            .HasMaxLength(ValidationConstants.MAX_DESKRIPTION_LENGTH)
            .IsRequired(false);

        builder.Property(a => a.InternalDefects)
            .HasMaxLength(ValidationConstants.MAX_DESKRIPTION_LENGTH)
            .IsRequired(false);

        builder.Property(a => a.ClientSign)
            .IsRequired(false);

        builder.Property(a => a.WorkerSign)
            .IsRequired(false);

        builder.HasOne(a => a.Worker)
            .WithMany(w => w.Acceptances)
            .HasForeignKey(a => a.WorkerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Order)
            .WithMany(o => o.Acceptances)
            .HasForeignKey(a => a.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
