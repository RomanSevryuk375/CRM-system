using CRMSystem.Core.Constants;
using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
{
    void IEntityTypeConfiguration<CarEntity>.Configure(EntityTypeBuilder<CarEntity> builder)
    {

        builder.ToTable("cars");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.OwnerId)
            .IsRequired();

        builder.Property(c => c.StatusId)
            //.HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.Brand)
            .HasMaxLength(ValidationConstants.MAX_BRAND_LENGTH)
            .IsRequired();

        builder.Property(c => c.Model)
            .HasMaxLength(ValidationConstants.MAX_MODEL_LENGTH)
            .IsRequired();

        builder.Property(c => c.YearOfManufacture)
            .IsRequired();

        builder.Property(c => c.VinNumber)
            .HasMaxLength(ValidationConstants.MAX_VIN_LENGTH)
            .IsRequired();

        builder.Property(c => c.StateNumber)
            .HasMaxLength(ValidationConstants.MAX_STATE_NUMBER_LENGTH)
            .IsRequired();

        builder.Property(c => c.Mileage)
            .IsRequired();

        builder.HasOne(c => c.Client)
            .WithMany(cl => cl.Cars)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Status)
            .WithMany(s => s.Cars)
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.VinNumber)
            .IsUnique();

        builder.HasIndex(c => c.StateNumber)
            .IsUnique();

    }
}
