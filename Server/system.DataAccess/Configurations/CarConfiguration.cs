using CRMSystem.Core.Models;
using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

internal class CarConfiguration : IEntityTypeConfiguration<CarEntity>
{
    void IEntityTypeConfiguration<CarEntity>.Configure(EntityTypeBuilder<CarEntity> builder)
    {
        builder.ToTable("cars");

        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id)
            .HasColumnName("car_id")
            .IsRequired();

        builder.Property(c => c.OwnerId)
            .HasColumnName("car_owner_id")
            .IsRequired();

        builder.Property(c => c.Brand)
            .HasColumnName("car_brand")
            .HasMaxLength(Car.MAX_BRAND_LENGTH)
            .IsRequired();

        builder.Property(c => c.Model)
            .HasColumnName("car_model")
            .HasMaxLength(Car.MAX_MODEL_LENGTH)
            .IsRequired();

        builder.Property(c => c.YearOfManufacture)
            .HasColumnName("car_year_of_manufacture")
            .IsRequired();

        builder.Property(c => c.VinNumber)
            .HasColumnName("car_vin_number")
            .IsRequired();

        builder.Property(c => c.StateNumber)
            .HasColumnName("car_state_number")
            .IsRequired();

        builder.Property(c => c.Mileage)
            .HasColumnName("car_mileage")
            .IsRequired();

        builder.HasOne(c => c.Client)
            .WithMany(client => client.Cars)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
