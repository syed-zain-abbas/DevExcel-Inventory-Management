using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Inventory.Domain.Enums;

namespace Inventory.Application.DTOs
{
    public class StockMovementInputDto
    {
        [Range(1, int.MaxValue,ErrorMessage = "Invalid product.")]
        public int ProductId { get; set; }

        [EnumDataType(typeof(MovementType),ErrorMessage = "Invalid movement type.")]
        public MovementType Type { get; set; }
        [Range(1, int.MaxValue,ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }
        
        [StringLength(500)]
        public string? Note { get; set; }
    }
}
