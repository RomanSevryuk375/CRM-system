using CRM.HR.Domain.Entities;
using CRM.HR.Infrastructure.Persistence;
using CRM.Shared.Abstractions.Abstractions;
using CRM.Shared.Abstractions.DDD.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.HR.Infrastructure.Persistence.Configurations;

internal sealed class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable("workers", HrDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Id,
                value => new WorkerId(value))
            .IsRequired();

        builder.Property(x => x.UserId)
            .HasConversion(
                id => id.Id,
                value => new UserId(value))
            .IsRequired();

        builder.Property(x => x.Name)
            .HasConversion(
                vo => vo.Value,
                dbVal => Name.Create(dbVal).Value)
            .HasMaxLength(Name.MaxLength)
            .IsRequired();

        builder.Property(x => x.Surname)
            .HasConversion(
                vo => vo.Value,
                dbVal => Name.Create(dbVal).Value)
            .HasMaxLength(Name.MaxLength)
            .IsRequired();

        builder.Property(x => x.HourlyRate)
            .HasConversion(
                vo => vo.Value,
                dbVal => Money.Create(dbVal).Value)
            .HasPrecision(Money.Precision, Money.Scale)
            .IsRequired();

        builder.Property(x => x.PhoneNumber)
            .HasConversion(
                vo => vo.Value,
                dbVal => PhoneNumber.Create(dbVal).Value)
            .HasMaxLength(PhoneNumber.MaxLength)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(
                vo => vo.Value,
                dbVal => Email.Create(dbVal).Value)
            .HasMaxLength(Email.MaxLength)
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

        builder.HasIndex(x => x.UserId).IsUnique();
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.PhoneNumber);

        builder.HasMany(x => x.Skills)
            .WithOne()
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata
            .FindNavigation(nameof(Worker.Skills))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
