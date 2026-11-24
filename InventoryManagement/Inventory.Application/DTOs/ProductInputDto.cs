using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.DTOs
{
    public class ProductInputDto
    {
        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int ReorderLevel { get; set; }
    }
}
