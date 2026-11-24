using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configurations
{
    public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(EntityTypeBuilder<StockMovement> builder)
        {
            builder.ToTable("StockMovements", table =>
            {
                table.HasCheckConstraint(
                    "CK_StockMovements_Quantity",
                    "[Quantity] > 0");

                table.HasCheckConstraint(
                    "CK_StockMovements_Type",
                    "[Type] IN (1, 2)");
            });

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Type).IsRequired();

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.Note).HasMaxLength(500);

            builder.Property(x => x.CreatedUtc).IsRequired();

            builder.HasOne(x => x.Product).WithMany(x => x.StockMovements).HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => new
            {
                x.ProductId,
                x.CreatedUtc
            });
        }
    }
}
