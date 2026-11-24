using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int ReorderLevel { get; set; }

        public int CurrentStock { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedUtc { get; set; }

        public bool IsLowStock => CurrentStock <= ReorderLevel;
    }
}
