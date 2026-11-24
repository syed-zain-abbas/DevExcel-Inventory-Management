using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.DTOs
{
    public class ProductInputDto
    {
        [Required(ErrorMessage = "SKU is required.")]
        public string Sku { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product name is required.")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reorder level cannot be negative.")]
        public int ReorderLevel { get; set; }
    }
}
