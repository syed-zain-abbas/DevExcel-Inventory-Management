using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Inventory.Application.DTOs
{
    public class ProductInputDto
    {
        [Required(ErrorMessage = "SKU is required.")]
        [StringLength(50)]
        public string Sku { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(200)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reorder level cannot be negative.")]
        public int ReorderLevel { get; set; }
    }
}
