using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", table =>
            {
                table.HasCheckConstraint(
                    "CK_Products_ReorderLevel",
                    "[ReorderLevel] >= 0");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Sku).IsRequired().HasMaxLength(50);

            builder.HasIndex(x => x.Sku).IsUnique();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.Property(x => x.ReorderLevel).HasDefaultValue(0);

            builder.Property(x => x.IsActive).HasDefaultValue(true);

            builder.Property(x => x.CreatedUtc).IsRequired();
        }
    }
}
