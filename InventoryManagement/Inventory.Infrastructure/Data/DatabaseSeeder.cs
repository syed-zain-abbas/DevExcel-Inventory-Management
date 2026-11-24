using System;
using System.Collections.Generic;
using System.Text;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Infrastructure.Data
{
    public static class DatabaseSeeder
    {
        public static List<Product> GetSeedProducts()
        {
            return new List<Product>
        {
            new Product
            {
                Sku = "LAP-001",
                Name = "Dell Laptop",
                Description = "Business laptop",
                ReorderLevel = 10,
                IsActive = true,
                StockMovements = new List<StockMovement>
                {
                    CreateMovement(MovementType.In, 100,
                        "Initial stock"),

                    CreateMovement(MovementType.Out, 20,
                        "Customer order")
                }
            },

            new Product
            {
                Sku = "MON-001",
                Name = "HP Monitor",
                Description = "24-inch monitor",
                ReorderLevel = 10,
                IsActive = true,
                StockMovements = new List<StockMovement>
                {
                    CreateMovement(MovementType.In, 20,
                        "Initial stock"),

                    CreateMovement(MovementType.Out, 15,
                        "Customer order")
                }
            },

            new Product
            {
                Sku = "KEY-001",
                Name = "Keyboard",
                ReorderLevel = 15,
                IsActive = true,
                StockMovements = new List<StockMovement>
                {
                    CreateMovement(MovementType.In, 40,
                        "Initial stock")
                }
            },

            new Product
            {
                Sku = "MOU-001",
                Name = "Wireless Mouse",
                ReorderLevel = 10,
                IsActive = true
            },

            new Product
            {
                Sku = "USB-001",
                Name = "USB Cable",
                ReorderLevel = 5,
                IsActive = false,
                StockMovements = new List<StockMovement>
                {
                    CreateMovement(MovementType.In, 20,
                        "Initial stock")
                }
            }
        };
        }

        private static StockMovement CreateMovement(
            MovementType type,
            int quantity,
            string note)
        {
            return new StockMovement
            {
                Type = type,
                Quantity = quantity,
                Note = note,
                CreatedUtc = DateTime.UtcNow
            };
        }
    }
}
