using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.DTOs
{
    public class ProductStockDto
    {
        public int ProductId { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public int CurrentStock { get; set; }

        public int ReorderLevel { get; set; }

        public bool IsActive { get; set; }

        public bool IsLowStock => CurrentStock <= ReorderLevel;
    }
}
