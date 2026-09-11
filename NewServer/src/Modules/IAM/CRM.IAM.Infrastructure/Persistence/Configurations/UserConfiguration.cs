using CRM.IAM.Domain.Entities;
using CRM.Shared.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.IAM.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users", IamDbContext.Schema);

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).IsRequired();

        builder.Property(x => x.Role)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Login)
            .HasMaxLength(User.MaxLoginLength)
            .IsRequired();

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(User.MaxPasswordHashLength)
            .IsRequired();

        builder.ConfigureBaseEntity();

        builder.HasIndex(x => x.Login).IsUnique();
        builder.HasIndex(x => x.Role);
    }
}
