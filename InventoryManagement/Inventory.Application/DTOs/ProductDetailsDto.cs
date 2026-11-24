using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Application.DTOs
{
    public class ProductDetailsDto
    {
        public ProductDto Product { get; set; } = new();

        public List<StockMovementDto> Movements { get; set; } = new();
    }
}
