using CRM.HR.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.HR.Infrastructure.Persistence.Configurations;

internal sealed class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.ToTable("workers", HrDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Surname).IsRequired();
        builder.Property(x => x.HourlyRate).IsRequired();
        builder.Property(x => x.PhoneNumber).IsRequired();
        builder.Property(x => x.Email).IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.UserId).IsUnique();
        builder.HasIndex(x => x.Email);
        builder.HasIndex(x => x.PhoneNumber);

        builder.HasMany(x => x.Skills)
            .WithOne()
            .HasForeignKey(x => x.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(w => w.Skills)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
