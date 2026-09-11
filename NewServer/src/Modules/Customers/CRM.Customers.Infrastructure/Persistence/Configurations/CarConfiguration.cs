using CRM.Customers.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Customers.Infrastructure.Persistence.Configurations;

internal sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("cars", CustomersDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.OwnerId).IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Brand)
            .HasMaxLength(Car.MaxBrandLength)
            .IsRequired();

        builder.Property(x => x.Model)
            .HasMaxLength(Car.MaxModelLength)
            .IsRequired();

        builder.Property(x => x.YearOfManufacture)
            .IsRequired();

        builder.Property(x => x.VinNumber)
            .HasMaxLength(Car.VinLength)
            .IsRequired();

        builder.Property(x => x.StateNumber)
            .HasMaxLength(Car.MaxStateNumberLength)
            .IsRequired();

        builder.Property(x => x.Mileage).IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.OwnerId);
        builder.HasIndex(x => x.VinNumber).IsUnique();
        builder.HasIndex(x => x.StateNumber);
    }
}
