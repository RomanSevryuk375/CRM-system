using CRM.Customers.Domain.Entities;
using CRM.Customers.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.Customers.Infrastructure.Persistence.Configurations;

internal sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("cars", CustomersDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new CarId(value))
            .IsRequired();

        builder.Property(x => x.OwnerId)
            .HasConversion(
                id => id.Id,
                value => new CustomerId(value))
            .IsRequired();

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

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

        builder.Property(x => x.Mileage)
            .IsRequired();

        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.DeletedAt).IsRequired(false);
        builder.Property(x => x.DeletedBy).IsRequired(false);

        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.CreatedBy).IsRequired();
        builder.Property(x => x.UpdatedAt).IsRequired(false);
        builder.Property(x => x.UpdatedBy).IsRequired(false);

        builder.Property(x => x.Version).IsConcurrencyToken();
        builder.HasQueryFilter(x => !x.IsDeleted);

        builder.HasIndex(x => x.OwnerId);
        builder.HasIndex(x => x.VinNumber).IsUnique();
        builder.HasIndex(x => x.StateNumber);
    }
}
