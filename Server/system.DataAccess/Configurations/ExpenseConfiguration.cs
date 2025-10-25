﻿using CRMSystem.DataAccess.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRMSystem.DataAccess.Configurations;

public class ExpenseConfiguration : IEntityTypeConfiguration<ExpenseEntity>
{
    void IEntityTypeConfiguration<ExpenseEntity>.Configure(EntityTypeBuilder<ExpenseEntity> builder)
    {
        builder.ToTable("expenses");

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Id)
            .HasColumnName("expense_id")
            .IsRequired();

        builder.Property(e => e.Date)
            .HasColumnName("expenses_date")
            .IsRequired();

        builder.Property(e => e.Category)
            .HasColumnName("expenses_category")
            .IsRequired();

        builder.Property(e => e.TaxId)
            .HasColumnName("tax_id")
            .IsRequired();

        builder.Property(e => e.UsedPartId)
            .HasColumnName("used_part_id")
            .IsRequired();

        builder.Property(e => e.ExpenseType)
            .HasColumnName("expenses_type")
            .IsRequired();

        builder.Property(e => e.Sum)
            .HasColumnName("expenses_sum")
            .IsRequired();

        builder.HasOne(t => t.Tax)
            .WithMany(e => e.Expenses)
            .HasForeignKey(e => e.TaxId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.UsedPart)
            .WithMany(e => e.Expenses)
            .HasForeignKey("used_part_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
